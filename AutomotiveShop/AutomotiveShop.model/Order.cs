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

        public OrderState OrderState { get; set; }

        public DateTime DateOfPurchase { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual List<ProductCopy> ProductsInOrder { get; set; }

        public virtual DeliveryAddress DeliveryAddress { get; set; }

        public Order()
        {
            ProductsInOrder = new List<ProductCopy>();
        }
    }

    public enum OrderState
    {
        New,
        Sent,
        Completed,
        Cancelled
    }
}