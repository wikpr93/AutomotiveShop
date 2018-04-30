using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Products
{
    public class ProductToBuyViewModel
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        [Display(Name = "Bought")]
        public int AlreadyBought { get; set; }
        public int ItemsAvailable { get; set; }
        public int ItemsInCart { get; set; }
        public int CopiesToBuy { get; set; }
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }
        [Display(Name = "Subcategory name")]
        public string SubcategoryName { get; set; }

        public ProductToBuyViewModel()
        {
            CopiesToBuy = 1;
        }
    }
}