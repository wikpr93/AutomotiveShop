using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Subcategories
{
    public class SubcategoryToEditViewModel
    {
        public Guid SubcategoryId { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
    }
}