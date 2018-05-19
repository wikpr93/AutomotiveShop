﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Products
{
    public class NewProductViewModel
    {
        public Guid ProductId { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Cena")]
        public double Price { get; set; }

        [Display(Name = "Dostępnych produktów")]
        public int ItemsAvailable { get; set; }
        
        public Guid SubcategoryId { get; set; }
    }
}