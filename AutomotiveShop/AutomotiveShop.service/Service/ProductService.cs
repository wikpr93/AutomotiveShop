using AutomotiveShop.model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutomotiveShop.service.ViewModels.Products;

namespace AutomotiveShop.service.Service
{
    public class ProductService
    {
        private AutomotiveShopDbContext _dbContext = new AutomotiveShopDbContext();

        public List<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetProductById(Guid? productId)
        {
            if (productId == null)
            {
                return null;
            }
            return _dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public Guid Create(NewProductViewModel model/*, ApplicationUser user*/)
        {
            Product newProduct = new Product();
            newProduct.ProductId = Guid.NewGuid();
            newProduct.Name = model.Name;
            newProduct.ItemsAvailable = model.ItemsAvailable;
            newProduct.Price = model.Price;
            newProduct.SubcategoryId = model.SubcategoryId;

            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();

            return newProduct.ProductId;
        }

        public void Edit(ProductToEditViewModel productToEdit)
        {
            Product product = _dbContext.Products.FirstOrDefault(c => c.ProductId == productToEdit.ProductId);
            product.Name = productToEdit.Name;
            product.Price = productToEdit.Price;
            product.ItemsAvailable = productToEdit.ItemsAvailable;
            _dbContext.Entry(product).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remove(Product productToRemove)
        {
            _dbContext.Products.Remove(productToRemove);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Buy(Product productToBuy, ApplicationUser user)
        {
            
            Order order = new Order()
            {
                OrderId = Guid.NewGuid(),
                OrderState = OrderState.New,
                DateOfPurchase = DateTime.Now,
                UserId = user.Id
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            ProductCopy boughtItem = new ProductCopy()
            {
                ProductCopyId = Guid.NewGuid(),
                ProductId = productToBuy.ProductId,
                Price = productToBuy.Price,
                OrderId = order.OrderId
            };
            productToBuy.ItemsAvailable--;
            _dbContext.ProductsCopies.Add(boughtItem);
            

            // todo shopping cart

            Order newOrder = new Order();
            List<ProductCopy> list = new List<ProductCopy>();
            list.Add(boughtItem);
            newOrder.OrderId = Guid.NewGuid();
            newOrder.DateOfPurchase = DateTime.Now;
            newOrder.ProductsInOrder = list;
            newOrder.UserId = user.Id;
            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();


        }



    }
}