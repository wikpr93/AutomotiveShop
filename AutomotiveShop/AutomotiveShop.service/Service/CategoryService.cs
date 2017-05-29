using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutomotiveShop.model;
using AutomotiveShop.service.ViewModels.Categories;

namespace AutomotiveShop.service.Service
{
    public class CategoryService
    {
        private AutomotiveShopDbContext _dbContext = new AutomotiveShopDbContext();

        public List<Category> GetCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetCategoryById(Guid? categoryId)
        {
            if (categoryId == null)
            {
                return null;
            }

            return _dbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }


        public Guid Create(NewCategoryViewModel model)
        {
            Category newCategory = new Category();
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.Name = model.Name;

            _dbContext.Categories.Add(newCategory);
            _dbContext.SaveChanges();

            return newCategory.CategoryId;
        }

        public void Edit(CategoryToEditViewModel categoryToEdit)
        {
            Category category = _dbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryToEdit.CategoryId);
            category.Name = categoryToEdit.Name;
            _dbContext.Entry(category).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remove(Category categoryToRemove)
        {
            _dbContext.Categories.Remove(categoryToRemove);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}