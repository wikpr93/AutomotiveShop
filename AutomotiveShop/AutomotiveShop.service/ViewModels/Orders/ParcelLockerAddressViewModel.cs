using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Orders
{
    public class ParcelLockerAddressViewModel
    {
        [Display(Name = "Ulica")]
        public string Street { get; set; }
        [Display(Name = "Kod pocztowy")]
        public string Postcode { get; set; }
        [Display(Name = "Miasto")]
        public string City { get; set; }
        public string Displayed { get; set; }
    }
}