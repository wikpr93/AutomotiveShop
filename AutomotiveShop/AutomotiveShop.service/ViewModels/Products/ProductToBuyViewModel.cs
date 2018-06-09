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

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Cena")]
        public string Price { get; set; }
        public string Image { get; set; }

        [Display(Name = "Kupionych")]
        public int AlreadyBought { get; set; }

        [Display(Name = "Dostępnych produktów")]
        public int ItemsAvailable { get; set; }

        [Display(Name = "Produktów w koszyku")]
        public int ItemsInCart { get; set; }
        public int CopiesToBuy { get; set; }
        [Display(Name = "Kategoria")]
        public string CategoryName { get; set; }
        [Display(Name = "Podkategoria")]
        public string SubcategoryName { get; set; }

        public ProductToBuyViewModel()
        {
            CopiesToBuy = 1;
        }
    }
}