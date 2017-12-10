using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Orders
{
    public class ParcelLockerAddressViewModel
    {
        [Display(Name = "Street")]
        public string Street { get; set; }
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
    }
}