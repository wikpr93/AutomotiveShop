using AutomotiveShop.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Orders
{
    public class NewOrderViewModel
    {
        public List<Product> Products { get; set; }
        public AutomotiveShop.model.DeliveryAddress DeliveryAddress { get; set; }
    }
}