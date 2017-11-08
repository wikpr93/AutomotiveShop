using AutomotiveShop.model;
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

        public Guid Create(NewOrderViewModel orderToCreate, ApplicationUser user)
        {
            Order newOrder = new Order();
            newOrder.OrderId = Guid.NewGuid();
            newOrder.DateOfPurchase = DateTime.Now;
            newOrder.DeliveryAddress = orderToCreate.DeliveryAddress;
            foreach(Product product in orderToCreate.Products)
            {
                ProductCopy newCopy = new ProductCopy();
                newCopy.ProductCopyId = Guid.NewGuid();
                newCopy.ProductId = product.ProductId;
                newCopy.Price = product.Price;
                newOrder.ProductsInOrder.Add(newCopy);
                _dbContext.ProductsCopies.Add(newCopy);
                _dbContext.SaveChanges();
            }
            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();
            return Guid.NewGuid();
        }
    }
}