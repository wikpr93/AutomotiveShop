﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomotiveShop.model
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ProductId { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        public int ItemsAvailable { get; set; }

        public byte[] Image { get; set; }

        public virtual List<ProductCopy> Copies { get; set; }

        public virtual List<ProductByCar> CarsByProduct { get; set; }

        [ForeignKey("Subcategory")]
        public Guid SubcategoryId { get; set; }
        public virtual Subcategory Subcategory { get; set; }

        public Product()
        {
            Copies = new List<ProductCopy>();
            CarsByProduct = new List<ProductByCar>();
            ItemsAvailable = 0;
        }
    }
}