using AutomotiveShop.service.ViewModels.Subcategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.ViewModels.Categories
{
    public class CategoryDetailsViewModel
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }
        public List<SubcategoryInCategoryViewModel> Subcategories { get; set; }

        public CategoryDetailsViewModel()
        {
            Subcategories = new List<SubcategoryInCategoryViewModel>();
        }
    }
}