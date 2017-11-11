using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Users
{
    public class OrderIndexViewModel
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string RelativeTime { get; set; }
    }
}