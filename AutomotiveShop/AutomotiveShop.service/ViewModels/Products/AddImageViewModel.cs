using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Products
{
    public class AddImageViewModel
    {
        public Guid ProductId { get; set; }
        public byte[] ByteImage { get; set; }

    }
}