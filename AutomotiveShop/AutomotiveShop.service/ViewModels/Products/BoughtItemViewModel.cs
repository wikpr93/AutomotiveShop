using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Products
{
    public class BoughtItemViewModel
    {
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
    }
}