using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutomotiveShop.model;
using AutomotiveShop.service.Service;
using AutomotiveShop.service.ViewModels.Categories;
using AutomotiveShop.service.ViewModels.Products;
using AutomotiveShop.service.ViewModels.Subcategories;

namespace AutomotiveShop.web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SubcategoriesController : Controller
    {
        private SubcategoryService _subcategoryService = new SubcategoryService();
        private CategoryService _categoryService = new CategoryService();

        // GET: Subcategories
        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Categories");
        }

        // GET: Subcategories/Details/5
        [AllowAnonymous]
        public ActionResult Details(Guid? subcategoryId)
        {
            if (subcategoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategory subcategory = _subcategoryService.GetSubcategoryById(subcategoryId);
            if (subcategory == null)
            {
                return HttpNotFound();
            }

            SubcategoryDetailsViewModel model = new SubcategoryDetailsViewModel()
            {
                SubcategoryId = subcategory.SubcategoryId,
                SubcategoryName = subcategory.Name
            };

            subcategory.Products.ForEach(p =>
            {
                model.Products.Add(new ProductViewModel()
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price.ToString("C", new CultureInfo("en-GB")),
                    CategoryId = p.Subcategory.Category.CategoryId,
                    CategoryName = p.Subcategory.Category.Name,
                    SubcategoryId = p.Subcategory.SubcategoryId,
                    SubcategoryName = p.Subcategory.Name
                });
            });

            return View(model);
        }

        // GET: Subcategories/Create
        public ActionResult Create(Guid? categoryId)
        {
            if (categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category currentCategory = _categoryService.GetCategoryById(categoryId);
            if (currentCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                TempData["CurrentCategory"] = currentCategory.Name;
            }
            NewCategoryViewModel categoryToCreate = new NewCategoryViewModel()
            {
                CategoryId = currentCategory.CategoryId
            };
            return View();
        }

        // POST: Subcategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubcategoryId,Name,CategoryId")] NewSubcategoryViewModel subcategoryToCreate)
        {
            if (ModelState.IsValid)
            {
                _subcategoryService.Create(subcategoryToCreate);
                return RedirectToAction("Index", "Categories");
            }

            //ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", subcategory.CategoryId);
            return View(subcategoryToCreate);
        }

        // GET: Subcategories/Edit/5
        public ActionResult Edit(Guid? subcategoryId)
        {
            if (subcategoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategory subcategory = _subcategoryService.GetSubcategoryById(subcategoryId);
            if (subcategory == null)
            {
                return HttpNotFound();
            }

            SubcategoryToEditViewModel subcategoryToEdit = new SubcategoryToEditViewModel()
            {
                SubcategoryId = subcategory.SubcategoryId,
                Name = subcategory.Name,
                CategoryName = subcategory.Category.Name
            };
            return View(subcategoryToEdit);
        }

        // POST: Subcategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubcategoryId,Name,CategoryId")] SubcategoryToEditViewModel subcategoryToEdit)
        {
            if (ModelState.IsValid)
            {
                _subcategoryService.Edit(subcategoryToEdit);
                return RedirectToAction("Index", "Categories");
            }
            return View(subcategoryToEdit);
        }

        // GET: Subcategories/Delete/5
        public ActionResult Delete(Guid? subcategoryId)
        {
            if (subcategoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategory subcategory = _subcategoryService.GetSubcategoryById(subcategoryId);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            return View(subcategory);
        }

        // POST: Subcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid subcategoryId)
        {
            Subcategory subcategory = _subcategoryService.GetSubcategoryById(subcategoryId);
            _subcategoryService.Remove(subcategory);
            return RedirectToAction("Index", "Categories");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _subcategoryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
