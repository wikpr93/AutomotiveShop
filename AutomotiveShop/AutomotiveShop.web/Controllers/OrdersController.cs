using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomotiveShop.model;
using AutomotiveShop.model.Infrastructure;
using AutomotiveShop.service.Service;
using AutomotiveShop.service.ViewModels.Orders;

namespace AutomotiveShop.web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private OrderService _orderService = new OrderService();
        private ProductService _productService = new ProductService();
        private UserService _userService = new UserService();

        public OrdersController()
        {

        }
       
        public ActionResult Create(/*DeliveryAddress deliveryAddress*/)
        {
            _orderService.Create(new DeliveryAddress(), _userService.ReturnUserByUsername(User.Identity.Name));
            return View("Index", "Home");
        }
        
        [AllowAnonymous]
        public ActionResult AddToCart(Guid productId)
        {
            _orderService.AddToCart(productId);
            return RedirectToAction("Details");
        }

        // GET: Orders
        public ActionResult Index()
        {
            return null;
            //var orders = db.Orders.Include(o => o.User);
            //return View(orders.ToList());
        }

        // GET: Orders/Details/5
        [AllowAnonymous]
        public ActionResult Details()
        {
            CartViewModel cart = new CartViewModel();
            foreach(ItemInCartViewModel item in _orderService.GetCart())
            {
                cart.Items.Add(item);
                cart.Price += item.Value;
            }
            return View(cart);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Order order = db.Orders.Find(id);
            //if (order == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(order);
        }
        

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,OrderState,DateOfPurchase,UserId")] Order order)
        {
            return null;
            //if (ModelState.IsValid)
            //{
            //    order.OrderId = Guid.NewGuid();
            //    db.Orders.Add(order);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", order.UserId);
            //return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(Guid? id)
        {
            return null;
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Order order = db.Orders.Find(id);
            //if (order == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", order.UserId);
            //return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,OrderState,DateOfPurchase,UserId")] Order order)
        {
            return null;
            //if (ModelState.IsValid)
            //{
            //    db.Entry(order).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", order.UserId);
            //return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(Guid? id)
        {
            return null;
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Order order = db.Orders.Find(id);
            //if (order == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            return null;
            //Order order = db.Orders.Find(id);
            //db.Orders.Remove(order);
            //db.SaveChanges();
            //return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            //base.Dispose(disposing);
        }
    }
}
