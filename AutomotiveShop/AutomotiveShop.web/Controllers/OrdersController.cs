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
using System.IO;
using AutomotiveShop.service.ViewModels.Products;

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
            AddressesToChooseViewModel model = new AddressesToChooseViewModel();
            foreach (DeliveryAddress address in _userService.ReturnUserByUsername(User.Identity.Name).DeliveryAddresses.ToList())
            {
                model.Addresses.Add(new DeliveryAddressViewModel()
                {
                    DeliveryAddressId = address.DeliveryAddressId,
                    CompanyName = address.CompanyName,
                    Name = (address.Name != null && address.Surname != null)?address.Name + " " + address.Surname:address.Name??address.Surname,
                    StreetName = address.StreetName,
                    City = (address.Postcode != null && address.City != null) ? address.Postcode + " " + address.City : address.Postcode ?? address.City,
                    PhoneNumber = address.PhoneNumber,
                    AdditionalInfo = address.AdditionalInfo
                });
            }
            
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://api.paczkomaty.pl/?do=listmachines_csv");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());

            string fileList = "";
            string[] tempStr;
            bool correct;
            while (sr.Peek() >= 0)
            {
                correct = true;
                fileList = sr.ReadLine();
                ParcelLockerAddressViewModel address = new ParcelLockerAddressViewModel();
                tempStr = fileList.Split(';');
                for (int i = 0; i < tempStr.Length; i++)
                {
                    if (tempStr.Length < 16 || (i >= 1 && i <= 4 && string.IsNullOrEmpty(tempStr[i])))
                    {
                        correct = false;
                        continue;
                    }
                    switch (i)
                    {
                        case 1:
                            address.Street = tempStr[i];
                            break;
                        case 2:
                            address.Street += " " + tempStr[i];
                            break;
                        case 3:
                            address.Postcode = tempStr[i];
                            break;
                        case 4:
                            address.City = tempStr[i];
                            break;
                    }
                }
                if (correct)
                {
                    address.Displayed = address.City + ", " + address.Street;
                    model.ParcelLockers.Add(address);
                }
            }
            sr.Close();
            model.ParcelLockers = model.ParcelLockers.OrderBy(o => o.City).ToList();
            return View("DeliveryAddress", model);
        }

        [HttpPost]
        public ActionResult ChosenParcelLocker(AddressesToChooseViewModel model)
        {
            Guid orderId = _orderService.Create(model.ParcelLockers.FirstOrDefault(p => p.Displayed.Equals(model.SelectedParcel)), _userService.ReturnUserByUsername(User.Identity.Name));
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["ItemsBought"] = "Dziękujemy za zakupy :) Możesz za nie zapłacić w dolnej sekcji na tej stronie";

            return RedirectToAction("Details", new { orderId });
        }

        public ActionResult Create(Guid deliveryAddressId)
        {
            Guid orderId = _orderService.Create(deliveryAddressId, _userService.ReturnUserByUsername(User.Identity.Name));

            if(orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            TempData["ItemsBought"] = "Dziękujemy za zakupy :) Możesz za nie zapłacić w dolnej sekcji na tej stronie";

            return RedirectToAction("Details", new { orderId });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddToCart(Guid? productId, int copiesToBuy)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!_orderService.AddToCart(productId, copiesToBuy))
            {
                TempData["AddingToCartFailed"] = "Nie udało się dodać produktu do koszyka. Sprawdź, czy posiadamy wystarczającą ilość!";
                return RedirectToAction("Details", "Products", new { productId = productId });
            };
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
                OrderState = order.OrderState,
                IsOwner = _userService.ReturnUserByUsername(User.Identity.Name).Id == order.UserId
            };
            switch ((int)model.OrderState)
            {
                case 0:
                    model.DisplayedOrderState = "Nowe";
                    break;
                case 1:
                    model.DisplayedOrderState = "Opłacone";
                    break;
                case 2:
                    model.DisplayedOrderState = "Wysłane";
                    break;
                case 3:
                    model.DisplayedOrderState = "Dostarczone";
                    break;
                default:
                    model.DisplayedOrderState = "Anulowane";
                    break;
            }
            DeliveryAddress address = _orderService.FindDeliveryAddressById(order.DeliveryAddressId);
            model.DeliveryAddress.CompanyName = address.CompanyName;
            model.DeliveryAddress.Name = (address.Name != null && address.Surname != null) ? address.Name + " " + address.Surname : address.Name ?? address.Surname;
            model.DeliveryAddress.StreetName = address.StreetName;
            model.DeliveryAddress.City = (address.Postcode != null && address.City != null) ? address.Postcode + " " + address.City : address.Postcode ?? address.City;
            model.DeliveryAddress.PhoneNumber = address.PhoneNumber;
            model.DeliveryAddress.AdditionalInfo = address.AdditionalInfo;

            switch ((int)model.OrderState)
            {
                case 0:
                    model.NextAction = "Zapłać za zamówienie";
                    break;
                case 1:
                    model.NextAction = "Oznacz jako wysłane";
                    break;
                case 2:
                    model.NextAction = "Oznacz jako dostarczone";
                    break;
                case 3:
                    model.NextAction = "Anuluj zamówienie";
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

        [AllowAnonymous]
        public ActionResult EmptyCart()
        {
            _orderService.EmptyCart();
            return RedirectToAction("Index", "Home");

        }

        public ActionResult ProcessOrder(Guid orderId, OrderState orderState, bool toCancellation)
        {
            _orderService.ProcessOrder(orderId, orderState, toCancellation);
            return RedirectToAction("Details", new { orderId });
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
