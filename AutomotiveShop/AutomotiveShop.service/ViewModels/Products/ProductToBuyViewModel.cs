using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Products
{
    public class ProductToBuyViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int AlreadyBought { get; set; }
        public int ItemsAvailable { get; set; }
        public int CopiesToBuy { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }

        public ProductToBuyViewModel()
        {
            CopiesToBuy = 1;
        }
    }
}