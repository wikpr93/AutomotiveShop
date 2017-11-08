using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.DeliveryAddress
{
    public class NewDeliveryAddressViewModel
    {
        public string CompanyName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string StreetName { get; set; }

        public string Postcode { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string AdditionalInfo { get; set; }
    }
}