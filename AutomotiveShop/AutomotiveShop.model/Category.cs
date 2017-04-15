using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomotiveShop.model
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public virtual List<Subcategory> Subcategories { get; set; }

        public Category()
        {
            Subcategories = new List<Subcategory>();
        }
    }
}