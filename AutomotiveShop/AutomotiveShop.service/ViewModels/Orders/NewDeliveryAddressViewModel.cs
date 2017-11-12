using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Orders
{
    public class NewDeliveryAddressViewModel
    {
        public Guid DeliveryAddressId { get; set; }

        [Display(Name="Company name")]
        public string CompanyName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [Display(Name = "Street name")]
        public string StreetName { get; set; }

        public string Postcode { get; set; }

        public string City { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Additional info")]
        public string AdditionalInfo { get; set; }
    }
}