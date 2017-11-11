using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomotiveShop.model
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }

        [Required]
        public OrderState OrderState { get; set; }

        [Required]
        public DateTime DateOfPurchase { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual List<ProductCopy> ProductsInOrder { get; set; }

        [Required]
        public Guid DeliveryAddressId { get; set; }

        public Order()
        {
            
            ProductsInOrder = new List<ProductCopy>();
        }
    }

    public enum OrderState
    {
        New,
        Paid,
        Sent,
        Completed,
        Cancelled
    }
}