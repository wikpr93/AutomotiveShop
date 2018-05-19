using AutomotiveShop.service.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Products
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Cena")]
        public string Price { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }

        [Display(Name = "Kategoria")]
        public string CategoryName { get; set; }
        public Guid SubcategoryId { get; set; }

        [Display(Name = "Podkategoria")]
        public string SubcategoryName { get; set; }
    }
}