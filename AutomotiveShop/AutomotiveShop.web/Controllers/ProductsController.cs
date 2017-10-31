using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomotiveShop.model;
using AutomotiveShop.service.Service;
using AutomotiveShop.service.ViewModels.Products;

namespace AutomotiveShop.web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductsController : Controller
    {
        private ProductService _productService = new ProductService();
        private CategoryService _categoryService = new CategoryService();
        private SubcategoryService _subcategoryService = new SubcategoryService();
        private UserService _userService = new UserService();


        // GET: Products
        public ActionResult Index()
        {
            return View(_productService.GetProducts());
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
            return View(product);
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

    }
}
