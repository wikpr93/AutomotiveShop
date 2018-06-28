using AutomotiveShop.model;
using AutomotiveShop.model.Infrastructure;
using AutomotiveShop.service.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.Service
{
    public class OrderService
    {
        private AutomotiveShopDbContext _dbContext = new AutomotiveShopDbContext();
        private SessionManager _sessionManager = new SessionManager();
        private ProductService _productService = new ProductService();


        public Order FindOrderById(Guid orderId)
        {
            return _dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public DeliveryAddress FindDeliveryAddressById(Guid deliveryAddressId)
        {
            return _dbContext.DeliveryAddresses.FirstOrDefault(da => da.DeliveryAddressId == deliveryAddressId);
        }

        public Guid Create(Guid deliveryAddressId, ApplicationUser user)
        {
            List<Product> products = new List<Product>();
            foreach (ItemInCartViewModel item in GetCart())
            {
                if (item.Quantity <= item.Product.ItemsAvailable)
                {
                    for (int i = 0; i < item.Quantity; i++)
                    {
                        products.Add(item.Product);
                    }
                }
                else
                {
                    throw new Exception("Not enough products in stock");
                }
            }
            Order newOrder = new Order();
            newOrder.OrderId = Guid.NewGuid();
            newOrder.DateOfPurchase = DateTime.Now;
            newOrder.OrderState = OrderState.New;
            newOrder.DeliveryAddressId = deliveryAddressId;
            newOrder.UserId = user.Id;
            _dbContext.Orders.Add(newOrder);
            foreach (Product product in products)
            {
                Product tempProd = _dbContext.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (tempProd.ItemsAvailable > 0)
                {
                    ProductCopy newCopy = new ProductCopy();
                    newCopy.ProductCopyId = Guid.NewGuid();
                    newCopy.ProductId = product.ProductId;
                    newCopy.Price = product.Price;
                    newCopy.OrderId = newOrder.OrderId;
                    _dbContext.ProductsCopies.Add(newCopy);
                    tempProd.ItemsAvailable--;
                    _dbContext.Entry(tempProd).Property(p => p.ItemsAvailable).IsModified = true;
                    _dbContext.SaveChanges();
                }
            }
            _dbContext.SaveChanges();
            EmptyCart();
            return newOrder.OrderId;

        }

        public Guid Create(ParcelLockerAddressViewModel parcel, ApplicationUser user)
        {
            DeliveryAddress address = _dbContext.DeliveryAddresses.FirstOrDefault(a => a.StreetName == parcel.Street && a.City == parcel.Street && a.Postcode == parcel.Postcode);
            
            if (address == null)
            {
                address = new DeliveryAddress()
                {
                    StreetName = parcel.Street,
                    Postcode = parcel.Postcode,
                    City = parcel.City
                };
                _dbContext.DeliveryAddresses.Add(address);
                _dbContext.SaveChanges();
            }

            List<Product> products = new List<Product>();
            foreach (ItemInCartViewModel item in GetCart())
            {
                if (item.Quantity <= item.Product.ItemsAvailable)
                {
                    for (int i = 0; i < item.Quantity; i++)
                    {
                        products.Add(item.Product);
                    }
                }
                else
                {
                    throw new Exception("Not enough products in stock");
                }
            }
            Order newOrder = new Order();
            newOrder.OrderId = Guid.NewGuid();
            newOrder.DateOfPurchase = DateTime.Now;
            newOrder.OrderState = OrderState.New;
            newOrder.DeliveryAddressId = address.DeliveryAddressId;
            newOrder.UserId = user.Id;
            _dbContext.Orders.Add(newOrder);

            foreach (Product product in products)
            {
                Product tempProd = _dbContext.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (tempProd.ItemsAvailable > 0)
                {
                    ProductCopy newCopy = new ProductCopy();
                    newCopy.ProductCopyId = Guid.NewGuid();
                    newCopy.ProductId = product.ProductId;
                    newCopy.Price = product.Price;
                    newCopy.OrderId = newOrder.OrderId;
                    _dbContext.ProductsCopies.Add(newCopy);
                    tempProd.ItemsAvailable--;
                    _dbContext.Entry(tempProd).Property(p => p.ItemsAvailable).IsModified = true;
                    _dbContext.SaveChanges();
                }
            }
            _dbContext.SaveChanges();
            EmptyCart();
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

        public List<DeliveryAddress> GetDeliveryAddressesByUser(ApplicationUser user)
        {
            return _dbContext.DeliveryAddresses.Where(o => o.UserId == user.Id).ToList();
        }

        public bool AddToCart(Guid? productId, int newQuantity)
        {
            if (productId == null)
            {
                return false;
            }

            bool newItem = false;

            List<ItemInCartViewModel> cart = GetCart();
            ItemInCartViewModel item = cart.Find(p => p.Product.ProductId == productId);

            if (item == null)
            {
                newItem = true;
                item = new ItemInCartViewModel()
                {
                    Product = _productService.GetProductById(productId),
                    Quantity = 0,
                    Value = 0
                };

                if(item.Product == null)
                {
                    return false;
                }
            }

            if (item != null && item.Quantity + newQuantity > _productService.GetProductById(productId).ItemsAvailable)
            {
                return false;
            }
            else if (item != null)
            {
                item.Quantity+= newQuantity;
                item.Value += newQuantity * _productService.GetProductById(item.Product.ProductId).Price;

                if(newItem)
                {
                    cart.Add(item);
                }
            }

            _sessionManager.Set(Consts.CartSessionKey, cart);

            return true;
        }

        public void ProcessOrder(Guid orderId, OrderState orderState, bool toCancellation)
        {
            // todo: processing state, not only incrementing
            Order order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if(order != null)
            {
                if(toCancellation)
                {
                    order.OrderState = OrderState.Cancelled;
                }
                else
                {
                    order.OrderState++;
                }
            }
            _dbContext.Entry(order).Property(p => p.OrderState).IsModified = true;
            _dbContext.SaveChanges();
        }

        public List<Order> GetOrdersByUser(ApplicationUser user)
        {
            return _dbContext.Orders.Where(o => o.UserId == user.Id).ToList();
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

        public void EmptyCart()
        {
            _sessionManager.Set<List<ItemInCartViewModel>>(Consts.CartSessionKey, null);
        }

        public string GetOrderNumberFromDate(DateTime date)
        {
            return date.Year.ToString("0000") + "-" + date.Month.ToString("00") + "-" + date.Day.ToString("00") + "-" + date.Hour.ToString("00") + "-" + date.Minute.ToString("00") + "-" + date.Second.ToString("00") + "-" + date.Millisecond.ToString("000");
        }

        public string GetRelativeTime(DateTime date)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - date.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "jedną sekundę temu" : ts.Seconds + " sekund(y) temu";

            if (delta < 2 * MINUTE)
                return "minutę temu";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minut(y) temu";

            if (delta < 90 * MINUTE)
                return "godzinę temu";

            if (delta < 24 * HOUR)
                return ts.Hours + " godzin(y) temu";

            if (delta < 48 * HOUR)
                return "wczoraj";

            if (delta < 30 * DAY)
                return ts.Days + " dni temu";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "miesiąc temu" : months + " miesiące(y) temu";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "rok temu" : years + " lat(a) temu";
            }
        }

        public void CreateDeliveryAddress(NewDeliveryAddressViewModel deliveryAddress, ApplicationUser user)
        {
            DeliveryAddress newAddress = new DeliveryAddress()
            {
                CompanyName = deliveryAddress.CompanyName,
                Name = deliveryAddress.Name,
                Surname = deliveryAddress.Surname,
                StreetName = deliveryAddress.StreetName,
                Postcode = deliveryAddress.Postcode,
                City = deliveryAddress.City,
                PhoneNumber = deliveryAddress.PhoneNumber,
                AdditionalInfo = deliveryAddress.AdditionalInfo,
                UserId = user.Id
            };
            _dbContext.DeliveryAddresses.Add(newAddress);
            _dbContext.SaveChanges();
        }

        public DeliveryAddress FindDeliveryAddressesById(Guid? deliveryaddressId)
        {
            throw new NotImplementedException();
        }

        public void EditDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            throw new NotImplementedException();
        }

        public void RemoveDeliveryAddress(Guid deliveryAddresId)
        {
            throw new NotImplementedException();
        }
    }
}