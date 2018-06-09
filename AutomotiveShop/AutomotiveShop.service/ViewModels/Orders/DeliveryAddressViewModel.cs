using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Orders
{
    public class DeliveryAddressViewModel
    {
        public Guid DeliveryAddressId { get; set; }

        [Display(Name = "Nazwa firmy")]
        public string CompanyName { get; set; }


        [Display(Name = "Imię i nazwisko")]
        public string Name { get; set; }

        [Display(Name = "Ulica")]
        public string StreetName { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Dodatkowe informacje")]
        public string AdditionalInfo { get; set; }
    }
}