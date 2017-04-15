using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomotiveShop.model
{
    public class ProductCopy
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductCopyId { get; set; }
        public double Price { get; set; }
        
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual List<ProductInOrder> OrdersByProduct { get; set; }

        public ProductCopy()
        {
            OrdersByProduct = new List<ProductInOrder>();
        }
    }
}