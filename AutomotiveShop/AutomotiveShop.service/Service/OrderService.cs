using AutomotiveShop.model;
using AutomotiveShop.model.Infrastructure;
using AutomotiveShop.service.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.Service
{
    public class OrderService
    {
        private AutomotiveShopDbContext _dbContext = new AutomotiveShopDbContext();
        private SessionManager _sessionManager = new SessionManager();

        public void DA()
        {
            Product prod = new Product();
            prod.Name = "name";
            prod.Price = 10.50;
            prod.SubcategoryId = _dbContext.Subcategories.FirstOrDefault(s => s.SubcategoryId != null).SubcategoryId;
            _dbContext.Products.Add(prod);
            _dbContext.SaveChanges();
            DeliveryAddress add = new DeliveryAddress();
            add.City = "City";
            add.StreetName = "Street";
            add.Postcode = "55555";
            add.UserId = _dbContext.Users.FirstOrDefault(u => u.Id != null).Id;
            _dbContext.DeliveryAddresses.Add(add);
            _dbContext.SaveChanges();

        }

        private ProductService _productService = new ProductService();

        public Guid Create(DeliveryAddress deliveryAddress, ApplicationUser user)
        {
            List<Product> products = new List<Product>();
            foreach (ItemInCartViewModel item in GetCart())
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    products.Add(item.Product);
                }
            }
            Order newOrder = new Order();
            newOrder.OrderId = Guid.NewGuid();
            newOrder.DateOfPurchase = DateTime.Now;
            newOrder.DeliveryAddress = _dbContext.DeliveryAddresses.FirstOrDefault(da => da.DeliveryAddressId != null);
            newOrder.UserId = user.Id;
            _dbContext.Orders.Add(newOrder);
            foreach (Product product in products)
            {
                if(product.ItemsAvailable > 0)
                {
                    ProductCopy newCopy = new ProductCopy();
                    newCopy.ProductCopyId = Guid.NewGuid();
                    newCopy.ProductId = product.ProductId;
                    newCopy.Price = product.Price;
                    newCopy.OrderId = newOrder.OrderId;
                    _dbContext.ProductsCopies.Add(newCopy);
                    product.ItemsAvailable--;
                }
                else
                {
                    throw new Exception("Item unavailable");
                }
            }
            _dbContext.SaveChanges();
            return newOrder.OrderId;
        }

        public List<ItemInCartViewModel> GetCart()
        {
            List<ItemInCartViewModel> cart;

            if (_sessionManager.Get<List<ItemInCartViewModel>>(Consts.CartSessionKey) == null)
            {
                cart = new List<ItemInCartViewModel>();
            }
            else
            {
                cart = _sessionManager.Get<List<ItemInCartViewModel>>(Consts.CartSessionKey) as List<ItemInCartViewModel>;
            }

            return cart;
        }

        public void AddToCart(Guid productId)
        {
            List<ItemInCartViewModel> cart = GetCart();
            ItemInCartViewModel item = cart.Find(p => p.Product.ProductId == productId);

            if (item != null)
            {
                item.Quantity++;
                item.Value += _productService.GetProductById(item.Product.ProductId).Price;
            }
            else
            {
                var productToAdd = _productService.GetProductById(productId);

                if (productToAdd != null)
                {
                    ItemInCartViewModel newItem = new ItemInCartViewModel()
                    {
                        Product = productToAdd,
                        Quantity = 1,
                        Value = productToAdd.Price
                    };
                    cart.Add(newItem);
                }
            }

            _sessionManager.Set(Consts.CartSessionKey, cart);
        }

        public int RemoveFromCart(Guid productId)
        {
            var cart = GetCart();
            var item = cart.Find(p => p.Product.ProductId == productId);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                    return item.Quantity;
                }
                else
                {
                    cart.Remove(item);
                }
            }

            return 0;
        }

        public double GetCartValue()
        {
            var cart = GetCart();
            return cart.Sum(i => i.Value);
        }

        public int GetNumberOfItemsInCart()
        {
            var cart = GetCart();
            int quantity = cart.Sum(c => c.Quantity);
            return quantity;
        }

        public void PustyKoszyk()
        {
            _sessionManager.Set<List<ItemInCartViewModel>>(Consts.CartSessionKey, null);
        }
    }
}