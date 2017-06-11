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
using AutomotiveShop.service.ViewModels.Categories;

namespace AutomotiveShop.web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        private CategoryService _categoryService = new CategoryService();
        // GET: Categories

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(_categoryService.GetCategories());
        }

        // GET: Categories/Details/5
        [AllowAnonymous]
        public ActionResult Details(Guid? categoryId)
        {
            if (categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name")] NewCategoryViewModel categoryToCreate)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Create(categoryToCreate);
                return RedirectToAction("Index");
            }

            return View(categoryToCreate);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(Guid? categoryId)
        {
            if (categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _categoryService.GetCategoryById(categoryId);

            if (category == null)
            {
                return HttpNotFound();
            }

            CategoryToEditViewModel categoryToEdit = new CategoryToEditViewModel()
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };
            return View(categoryToEdit);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name")] CategoryToEditViewModel category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Edit(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(Guid? categoryId)
        {
            if (categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _categoryService.GetCategoryById(categoryId);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid categoryId)
        {
            Category category = _categoryService.GetCategoryById(categoryId);
            _categoryService.Remove(category);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _categoryService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
