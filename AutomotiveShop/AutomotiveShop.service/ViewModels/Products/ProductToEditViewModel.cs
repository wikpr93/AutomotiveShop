using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Products
{
    public class ProductToEditViewModel
    {
        public Guid ProductId { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Cena")]
        public double Price { get; set; }

        [Display(Name = "Dostępnych produktów")]
        public int ItemsAvailable { get; set; }

        [Display(Name = "Kategoria")]
        public string CategoryName { get; set; }

        [Display(Name = "Podkategoria")]
        public string SubcategoryName { get; set; }

    }
}