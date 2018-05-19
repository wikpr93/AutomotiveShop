using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Subcategories
{
    public class SubcategoryToEditViewModel
    {
        public Guid SubcategoryId { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Kategoria")]
        public string CategoryName { get; set; }
    }
}