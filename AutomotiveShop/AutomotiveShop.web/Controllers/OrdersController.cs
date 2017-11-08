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
    public class OrdersController : Controller
    {
        private OrderService _orderService = new OrderService();
        private ProductService _productService = new ProductService();
        private UserService _userService = new UserService();
        private ISessionManager _sessionManager;

        public OrdersController(ISessionManager session)
        {
            _sessionManager = session;
        }

        public List<ItemInCartViewModel> GetCart()
        {
            List<ItemInCartViewModel> cart;

            if (_sessionManager.Get<List<ItemInCartViewModel>>(Consts.CartSessionKey) == null)
            {
                cart = new List<ItemInCartViewModel>();
            }
            else
            {
                cart = _sessionManager.Get<List<ItemInCartViewModel>>(Consts.CartSessionKey) as List<ItemInCartViewModel>;
            }

            return cart;
        }

        public void AddToCart(Guid productId)
        {
            List<ItemInCartViewModel> cart = GetCart();
            ItemInCartViewModel item = cart.Find(p => p.Product.ProductId == productId);

            if (item != null)
            {
                item.Quantity++;
                item.Value += _productService.GetProductById(item.Product.ProductId).Price;
            }
            else
            {
                var productToAdd = _productService.GetProductById(productId);

                if (productToAdd != null)
                {
                    ItemInCartViewModel newItem = new ItemInCartViewModel()
                    {
                        Product = productToAdd,
                        Quantity = 1,
                        Value = productToAdd.Price
                    };
                    cart.Add(newItem);
                }
            }

            _sessionManager.Set(Consts.CartSessionKey, cart);
        }

        public int RemoveFromCart(Guid productId)
        {
            var cart = GetCart();
            var item = cart.Find(p => p.Product.ProductId == productId);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                    return item.Quantity;
                }
                else
                {
                    cart.Remove(item);
                }
            }

            return 0;
        }

        public double GetCartValue()
        {
            var cart = GetCart();
            return cart.Sum(i => i.Value);
        }

        public int GetNumberOfItemsInCart()
        {
            var cart = GetCart();
            int quantity = cart.Sum(c => c.Quantity);
            return quantity;
        }

        public ActionResult Create(DeliveryAddress deliveryAddress)
        {
            List<Product> products = new List<Product>();
            foreach(ItemInCartViewModel item in GetCart())
            {
                for(int i=0; i<item.Quantity; i++)
                {
                    products.Add(item.Product);
                }
            }
            NewOrderViewModel orderToCreate = new NewOrderViewModel();
            _orderService.Create(orderToCreate, _userService.ReturnUserByUsername(User.Identity.Name));

            return null;
        }

        public void PustyKoszyk()
        {
            _sessionManager.Set<List<ItemInCartViewModel>>(Consts.CartSessionKey, null);
        }
        // GET: Orders
        public ActionResult Index()
        {
            return null;
            //var orders = db.Orders.Include(o => o.User);
            //return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(Guid? id)
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
