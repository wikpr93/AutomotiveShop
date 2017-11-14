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
using System.Web.WebPages;

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

        public ActionResult ChooseDeliveryAddress()
        {
            List<DeliveryAddressViewModel> model = new List<DeliveryAddressViewModel>();
            foreach (DeliveryAddress address in _userService.ReturnUserByUsername(User.Identity.Name).DeliveryAddresses.ToList())
            {
                model.Add(new DeliveryAddressViewModel()
                {
                    DeliveryAddressId = address.DeliveryAddressId,
                    CompanyName = address.CompanyName,
                    Name = address.Name,
                    Surname = address.Surname,
                    StreetName = address.StreetName,
                    Postcode = address.Postcode,
                    City = address.City,
                    PhoneNumber = address.PhoneNumber,
                    AdditionalInfo = address.PhoneNumber
                });
            }
            //List<string> model = new List<string>();
            //foreach (DeliveryAddress address in _userService.ReturnUserByUsername(User.Identity.Name).DeliveryAddresses.ToList())
            //{
            //    string add = "";
            //    if (!address.CompanyName.IsEmpty())
            //    {
            //        add += address.CompanyName + Environment.NewLine;
            //    }
            //    if (!address.Name.IsEmpty())
            //    {
            //        add += address.Name + Environment.NewLine;
            //    }
            //    if (!address.Surname.IsEmpty())
            //    {
            //        add += address.Surname + Environment.NewLine;
            //    }
            //    if (!address.StreetName.IsEmpty())
            //    {
            //        add += address.StreetName + Environment.NewLine + "<br />";
            //    }
            //    if (!address.Postcode.IsEmpty())
            //    {
            //        add += address.Postcode + Environment.NewLine;
            //    }
            //    if (!address.City.IsEmpty())
            //    {
            //        add += address.City + Environment.NewLine;
            //    }
            //    if (!address.PhoneNumber.IsEmpty())
            //    {
            //        add += address.PhoneNumber + Environment.NewLine;
            //    }
            //    if (!address.AdditionalInfo.IsEmpty())
            //    {
            //        add += address.AdditionalInfo + Environment.NewLine;
            //    }
            //    model.Add(add);
            //}
            return View("DeliveryAddress", model);
        }

        public ActionResult Create(Guid deliveryAddressId)
        {
            _orderService.Create(deliveryAddressId, _userService.ReturnUserByUsername(User.Identity.Name));
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult AddToCart(Guid productId)
        {
            _orderService.AddToCart(productId);
            return RedirectToAction("Index");
        }

        // GET: Orders
        [AllowAnonymous]
        public ActionResult Index()
        {
            CartViewModel cart = new CartViewModel();
            foreach (ItemInCartViewModel item in _orderService.GetCart())
            {
                cart.Items.Add(item);
                cart.Price += item.Value;
            }
            return View(cart);
            //var orders = db.Orders.Include(o => o.User);
            //return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(Guid orderId)
        {
            Order order = _orderService.FindOrderById(orderId);
            OrderDetailsViewModel model = new OrderDetailsViewModel()
            {
                OrderId = order.OrderId,
                DateOfPurchase = order.DateOfPurchase,
                DisplayedDateOfPurchase = order.DateOfPurchase.Year.ToString("0000") + "-" + order.DateOfPurchase.Month.ToString("00") + "-" + order.DateOfPurchase.Day.ToString("00") + " " + order.DateOfPurchase.Hour.ToString("00") + ":" + order.DateOfPurchase.Minute.ToString("00"),
                OrderState = order.OrderState
            };
            DeliveryAddress address = _orderService.FindDeliveryAddressById(order.DeliveryAddressId);
            model.DeliveryAddress.CompanyName = address.CompanyName;
            model.DeliveryAddress.Name = address.Name;
            model.DeliveryAddress.Surname = address.Surname;
            model.DeliveryAddress.StreetName = address.StreetName;
            model.DeliveryAddress.Postcode = address.Postcode;
            model.DeliveryAddress.City = address.City;
            model.DeliveryAddress.PhoneNumber = address.PhoneNumber;
            model.DeliveryAddress.AdditionalInfo = address.AdditionalInfo;
            switch ((int)model.OrderState)
            {
                case 0:
                    model.NextAction = "Pay for order";
                    break;
                case 1:
                    model.NextAction = "Mark as sent";
                    break;
                case 2:
                    model.NextAction = "Mark as received";
                    break;
                case 3:
                    model.NextAction = "Cancel order";
                    break;
                default:
                    model.NextAction = String.Empty;
                    break;
            }
            foreach (ProductCopy copy in order.ProductsInOrder)
            {
                var item = model.Items.Find(c => c.Product.ProductId == copy.ProductId);
                if (item != null)
                {
                    item.Quantity++;
                    item.Value += copy.Price;
                }
                else
                {
                    model.Items.Add(new ItemInCartViewModel()
                    {
                        Product = copy.Product,
                        Quantity = 1,
                        Value = copy.Price
                    });
                }
                model.Price += copy.Price;
            }
            return View(model);
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

        public ActionResult EmptyCart()
        {
            _orderService.EmptyCart();
            return RedirectToAction("Index", "Home");

        }

        public ActionResult ProcessOrder(Guid orderId, OrderState orderState, bool toCancellation)
        {
            _orderService.ProcessOrder(orderId, orderState, toCancellation);
            return RedirectToAction("Details", new { orderId = orderId });
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

        // GET: DeliveryAddresses/Create
        public ActionResult CreateDeliveryAddress()
        {
            return View();
        }

        // POST: DeliveryAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDeliveryAddress([Bind(Include = "CompanyName,Name,Surname,StreetName,Postcode,City,PhoneNumber,AdditionalInfo")] NewDeliveryAddressViewModel deliveryAddress)
        {
            if (ModelState.IsValid)
            {
                _orderService.CreateDeliveryAddress(deliveryAddress, _userService.ReturnUserByUsername(User.Identity.Name));
                return RedirectToAction("ChooseDeliveryAddress");
            }
            return View(deliveryAddress);
        }

        // GET: DeliveryAddresses/Edit/5
        public ActionResult EditDeliveryAddress(Guid? deliveryaddressId)
        {
            if (deliveryaddressId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryAddress deliveryAddress = _orderService.FindDeliveryAddressesById(deliveryaddressId);
            if (deliveryAddress == null)
            {
                return HttpNotFound();
            }
            return View(deliveryAddress);
        }

        // POST: DeliveryAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDeliveryAddress([Bind(Include = "DeliveryAddressId,CompanyName,Name,Surname,StreetName,Postcode,City,PhoneNumber,AdditionalInfo,UserId")] DeliveryAddress deliveryAddress)
        {
            if (ModelState.IsValid)
            {
                _orderService.EditDeliveryAddress(deliveryAddress);
                return RedirectToAction("Index");
            }
            return View(deliveryAddress);
        }

        // GET: DeliveryAddresses/Delete/5
        public ActionResult DeleteDeliveryAddress(Guid? deliveryAddresId)
        {
            if (deliveryAddresId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryAddress deliveryAddress = _orderService.FindDeliveryAddressesById(deliveryAddresId);
            if (deliveryAddress == null)
            {
                return HttpNotFound();
            }
            return View(deliveryAddress);
        }

        // POST: DeliveryAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedDeliveryAddress(Guid deliveryAddresId)
        {
            _orderService.RemoveDeliveryAddress(deliveryAddresId);
            return RedirectToAction("Index");
        }
    }



}
