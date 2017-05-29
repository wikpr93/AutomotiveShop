using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Products
{
    public class NewProductViewModel
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        [Display(Name = "Items available")]
        public int ItemsAvailable { get; set; }
        
        public Guid SubcategoryId { get; set; }
    }
}