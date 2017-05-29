using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutomotiveShop.model;
using AutomotiveShop.service.ViewModels.Subcategories;

namespace AutomotiveShop.service.Service
{
    public class SubcategoryService
    {
        private AutomotiveShopDbContext _dbContext = new AutomotiveShopDbContext();

        public List<Subcategory> GetSubcategories()
        {
            return _dbContext.Subcategories.ToList();
        }

        public Subcategory GetSubcategoryById(Guid? subcategoryId)
        {
            if (subcategoryId == null)
            {
                return null;
            }

            return _dbContext.Subcategories.FirstOrDefault(s => s.SubcategoryId == subcategoryId);
        }

        public Guid Create(NewSubcategoryViewModel model)
        {
            Subcategory newSubcategory = new Subcategory()
            {
                SubcategoryId = Guid.NewGuid(),
                Name = model.Name,
                CategoryId = model.CategoryId
            };

            _dbContext.Subcategories.Add(newSubcategory);
            _dbContext.SaveChanges();

            return newSubcategory.SubcategoryId;
        }

        public void Edit(SubcategoryToEditViewModel subcategoryToEdit)
        {
            Subcategory subcategory =
                _dbContext.Subcategories.FirstOrDefault(s => s.SubcategoryId == subcategoryToEdit.SubcategoryId);
            subcategory.Name = subcategoryToEdit.Name;
            _dbContext.Entry(subcategory).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remove(Subcategory subcategoryToRemove)
        {
            _dbContext.Subcategories.Remove(subcategoryToRemove);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}