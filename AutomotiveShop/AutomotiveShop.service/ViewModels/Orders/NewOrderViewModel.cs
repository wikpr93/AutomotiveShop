using AutomotiveShop.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Orders
{
    public class NewOrderViewModel
    {
        public List<Product> Products { get; set; }

        [Display(Name = "Adres dostawy")]
        public DeliveryAddress DeliveryAddress { get; set; }
    }
}