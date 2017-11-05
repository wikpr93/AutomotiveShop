using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomotiveShop.model
{
    public class DeliveryAddress
    {
        public Guid DeliveryAddressId { get; set; }

        public string CompanyName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string StreetName { get; set; }

        public string Postcode { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string AdditionalInfo { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}