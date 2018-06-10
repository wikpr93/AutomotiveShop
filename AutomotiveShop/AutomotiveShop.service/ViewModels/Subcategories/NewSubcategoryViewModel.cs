using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Subcategories
{
    public class NewSubcategoryViewModel
    {
        public Guid SubcategoryId { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        public Guid CategoryId { get; set; }

        [Display(Name="Nazwa kategorii")]
        public string CategoryName { get; set; }
    }
}