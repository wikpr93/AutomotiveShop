using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Products
{
    public class AddImageViewModel
    {
        public Guid ProductId { get; set; }

        [Display(Name = "")]
        public byte[] ByteImage { get; set; }

    }
}