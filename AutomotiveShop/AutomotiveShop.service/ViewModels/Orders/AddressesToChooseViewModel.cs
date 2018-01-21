using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Orders
{
    public class AddressesToChooseViewModel
    {
        public List<DeliveryAddressViewModel> Addresses { get; set; }
        public List<ParcelLockerAddressViewModel> ParcelLockers { get; set; }

        public AddressesToChooseViewModel()
        {
            Addresses = new List<DeliveryAddressViewModel>();
            ParcelLockers = new List<ParcelLockerAddressViewModel>();
        }
    }
}