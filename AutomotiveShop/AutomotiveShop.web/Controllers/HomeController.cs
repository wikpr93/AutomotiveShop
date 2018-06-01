using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomotiveShop.model;
using AutomotiveShop.service.Service;
using AutomotiveShop.service.ViewModels.Products;

namespace AutomotiveShop.web.Controllers
{
    public class HomeController : Controller
    {
        private ProductService _productService = new ProductService();

        private const int _numberOfProductsOnIndexPage = 10;
        public ActionResult Index()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();
            List<Product> products = _productService.GetProducts();
            Random rnd = new Random();
            
            for (int i = 0; i < _numberOfProductsOnIndexPage; i++)
            {
                if(products.Count == 0)
                {
                    break;
                }

                Product randomProduct = products.ElementAt(rnd.Next(products.Count - 1));
                if (randomProduct.ItemsAvailable > 0)
                {
                    model.Add(new ProductViewModel()
                    {
                        ProductId = randomProduct.ProductId,
                        Name = randomProduct.Name,
                        Price = randomProduct.Price.ToString("C", new CultureInfo("pl-PL")),
                        Image = (randomProduct.Image != null)?(String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(randomProduct.Image))) :("data:image/jpg;base64,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"),
                        CategoryId = randomProduct.Subcategory.Category.CategoryId,
                        CategoryName = randomProduct.Subcategory.Category.Name,
                        SubcategoryId = randomProduct.Subcategory.SubcategoryId,
                        SubcategoryName = randomProduct.Subcategory.Name
                    });
                }
                else
                {
                    products.Remove(randomProduct);
                    i--;
                    continue;
                }
            }
            
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}