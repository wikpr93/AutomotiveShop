using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Categories
{
    public class NewCategoryViewModel
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }
    }
}