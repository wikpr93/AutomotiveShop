using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomotiveShop.model
{
    public class DeliveryAddress
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DeliveryAddressId { get; set; }

        public string CompanyName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Number of the building is required")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string AdditionalInfo { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}