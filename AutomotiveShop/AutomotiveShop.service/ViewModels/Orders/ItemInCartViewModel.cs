using AutomotiveShop.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Orders
{
    public class ItemInCartViewModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Value { get; set; }
    }
}