using System;
using System.Collections.Generic;
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
                        Price = randomProduct.Price.ToString("C", new CultureInfo("en-GB")),
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