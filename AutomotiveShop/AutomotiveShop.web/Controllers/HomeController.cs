using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomotiveShop.model;
using AutomotiveShop.service.Service;

namespace AutomotiveShop.web.Controllers
{
    public class HomeController : Controller
    {
        private OrderService _ordService = new OrderService();
        public ActionResult Index()
        {
            AutomotiveShopDbContext context = new AutomotiveShopDbContext();
            return View(context.Categories?.ToList());
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