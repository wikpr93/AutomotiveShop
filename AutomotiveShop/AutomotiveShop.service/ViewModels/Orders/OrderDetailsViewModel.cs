using AutomotiveShop.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Orders
{
    public class OrderDetailsViewModel
    {
        public Guid OrderId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public List<ItemInCartViewModel> Items { get; set; }
        public double Price { get; set; }
        public OrderState OrderState { get; set; }
        public string NextAction { get; set; }
        public DeliveryAddressViewModel DeliveryAddress { get; set; }
        public OrderDetailsViewModel()
        {
            Items = new List<ItemInCartViewModel>();
            DeliveryAddress = new DeliveryAddressViewModel();
        }

    }
}