using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Categories
{
    public class CategoryToEditViewModel
    {
        public Guid CategoryId { get; set; }

        [Display(Name="Nazwa")]
        public string Name { get; set; }
    }
}