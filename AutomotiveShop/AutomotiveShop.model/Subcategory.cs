using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomotiveShop.model
{
    public class Subcategory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SubcategoryId { get; set; }

        [Display(Name = "Subcategory")]
        [Required(ErrorMessage = "Subcategory name is required")]
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public Subcategory()
        {
            Products = new List<Product>();
        }



    }
}