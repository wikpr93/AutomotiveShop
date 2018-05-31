using AutomotiveShop.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Orders
{
    public class OrderDetailsViewModel
    {
        public Guid OrderId { get; set; }
        
        [Display(Name = "Data zakupu")]
        public DateTime DateOfPurchase { get; set; }
        
        public string DisplayedDateOfPurchase { get; set; }
        public List<ItemInCartViewModel> Items { get; set; }

        [Display(Name = "Cena")]
        public double Price { get; set; }

        public bool IsOwner { get; set; }
        public OrderState OrderState { get; set; }

        public string DisplayedOrderState { get; set; }
        public string NextAction { get; set; }
        
        [Display(Name = "Adres dostawy")]
        public DeliveryAddressViewModel DeliveryAddress { get; set; }
        public OrderDetailsViewModel()
        {
            Items = new List<ItemInCartViewModel>();
            DeliveryAddress = new DeliveryAddressViewModel();
        }

    }
}