using AutomotiveShop.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Orders
{
    public class ItemInCartViewModel
    {
        public Product Product { get; set; }

        [Display(Name = "Ilość")]
        public int Quantity { get; set; }

        [Display(Name = "Wartość")]
        public double Value { get; set; }
    }
}