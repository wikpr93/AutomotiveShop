using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Orders
{
    public class CartViewModel
    {
        public List<ItemInCartViewModel> Items { get; set; }
        public double Price { get; set; }
        public CartViewModel()
        {
            Items = new List<ItemInCartViewModel>();
        }
    }
}