﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomotiveShop.model;
using AutomotiveShop.service.Service;
using AutomotiveShop.service.ViewModels.Orders;
using AutomotiveShop.service.ViewModels.Products;

namespace AutomotiveShop.web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductsController : Controller
    {
        private ProductService _productService = new ProductService();
        private CategoryService _categoryService = new CategoryService();
        private SubcategoryService _subcategoryService = new SubcategoryService();
        private OrderService _orderService = new OrderService();
        private UserService _userService = new UserService();


        // GET: Products
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public ActionResult Details(Guid? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _productService.GetProductById(productId);

            if (product == null)
            {
                return HttpNotFound();
            }

            List<ItemInCartViewModel> cart = _orderService.GetCart();
            ItemInCartViewModel item = cart.Find(p => p.Product.ProductId == productId);

            byte[] img = _productService.GetProducts().FirstOrDefault(p => p.Image != null).Image;
            var base64 = Convert.ToBase64String(img);
            var imgSrc = String.Format("data:image/jpg;base64,{0}", base64);

            ProductToBuyViewModel model = new ProductToBuyViewModel()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                CategoryName = product.Subcategory.Category.Name,
                SubcategoryName = product.Subcategory.Name,
                Price = product.Price,
                Image = imgSrc,
                ItemsAvailable = product.ItemsAvailable,
                ItemsInCart = (item != null)?item.Quantity:0,
                AlreadyBought = product.Copies.Count
            };
            return View(model);
        }

        // GET: Products/Create
        public ActionResult Create(Guid? subcategoryId)
        {
            if (subcategoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategory currentSubcategory = _subcategoryService.GetSubcategoryById(subcategoryId);
            if (currentSubcategory == null)
            {
                return HttpNotFound();
            }

            Category currentCategory = _categoryService.GetCategoryById(currentSubcategory.Category.CategoryId);

            if (currentCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                TempData["CurrentCategory"] = currentCategory.Name;
                TempData["CurrentSubcategory"] = currentSubcategory.Name;
            }
            NewProductViewModel productToCreate = new NewProductViewModel();

            productToCreate.SubcategoryId = (Guid)subcategoryId;

            return View(productToCreate);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Price,ItemsAvailable,CategoryId,SubcategoryId")] NewProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                product.ProductId = Guid.NewGuid();
                _productService.Create(product);
                return RedirectToAction("Index", "Home");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(Guid? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _productService.GetProductById(productId);

            if (product == null)
            {
                return HttpNotFound();
            }

            ProductToEditViewModel productToEdit = new ProductToEditViewModel()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                ItemsAvailable = product.ItemsAvailable,
                CategoryName = product.Subcategory.Category.Name,
                SubcategoryName = product.Subcategory.Name

            };
            //ViewBag.SubcategoryId = new SelectList(db.Subcategories, "SubcategoryId", "Name", product.SubcategoryId);

            return View(productToEdit);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Price,ItemsAvailable")] ProductToEditViewModel product)
        {
            if (ModelState.IsValid)
            {
                _productService.Edit(product);
                return RedirectToAction("Index");
            }
            //ViewBag.SubcategoryId = new SelectList(db.Subcategories, "SubcategoryId", "Name", product.SubcategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(Guid? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _productService.GetProductById(productId);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid productId)
        {
            Product product = _productService.GetProductById(productId);
            _productService.Remove(product);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productService.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult Buy(Guid? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product productToBuy = _productService.GetProductById(productId);

            if (productToBuy == null)
            {
                return HttpNotFound();
            }

            _productService.Buy(productToBuy, _userService.ReturnUserByUsername(User.Identity.Name));

            BoughtItemViewModel model = new BoughtItemViewModel()
            {
                Name = productToBuy.Name
            };
            return View("Bought", model);
        }


        public ActionResult AddImage(Guid? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = _productService.GetProductById(productId);

            AddImageViewModel model = new AddImageViewModel()
            {
                ProductId = (Guid)productId
            };

            return View(model);

        }

        [HttpPost]
        public ActionResult AddImage(AddImageViewModel model, HttpPostedFileBase image1)
        {
            //if (productId == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            
            //_productService.AddImage(productId, img);
            if(image1 != null && 
                (image1.ContentType.Equals("image/png") || image1.ContentType.Equals("image/gif") 
                || image1.ContentType.Equals("image/jpg") || image1.ContentType.Equals("image/jpeg")))
            {
                model.ByteImage = new byte[image1.InputStream.Length];
                image1.InputStream.Read(model.ByteImage, 0, image1.ContentLength);
                _productService.AddImage(model);
            }


            return View("Index", "Home");
        }

    }
}
