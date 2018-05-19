using AutomotiveShop.service.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Subcategories
{
    public class SubcategoryDetailsViewModel
    {
        public Guid SubcategoryId { get; set; }

        [Display(Name = "Nazwa")]
        public string SubcategoryName { get; set; }
        public List<ProductViewModel> Products { get; set; }
        
        public SubcategoryDetailsViewModel()
        {
            Products = new List<ProductViewModel>();
        }
    }
}