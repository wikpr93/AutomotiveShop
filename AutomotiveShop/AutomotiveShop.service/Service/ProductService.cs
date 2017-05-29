using AutomotiveShop.model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutomotiveShop.service.ViewModels.Products;

namespace AutomotiveShop.service.Service
{
    public class ProductService
    {
        private AutomotiveShopDbContext _dbContext = new AutomotiveShopDbContext();

        public List<Product> GetProducts()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetProductById(Guid? productId)
        {
            if (productId == null)
            {
                return null;
            }
            return _dbContext.Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public Guid Create(NewProductViewModel model/*, ApplicationUser user*/)
        {
            Product newProduct = new Product();
            newProduct.ProductId = Guid.NewGuid();
            newProduct.Name = model.Name;
            newProduct.ItemsAvailable = model.ItemsAvailable;
            newProduct.Price = model.Price;
            newProduct.SubcategoryId = model.SubcategoryId;

            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();

            return newProduct.ProductId;
        }

        public void Edit(ProductToEditViewModel productToEdit)
        {
            Product product = _dbContext.Products.FirstOrDefault(c => c.ProductId == productToEdit.ProductId);
            product.Name = productToEdit.Name;
            product.Price = productToEdit.Price;
            product.ItemsAvailable = productToEdit.ItemsAvailable;
            _dbContext.Entry(product).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Remove(Product productToRemove)
        {
            _dbContext.Products.Remove(productToRemove);
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Buy(Product productToBuy)
        {
            ProductCopy boughtItem = new ProductCopy()
            {
                ProductCopyId = Guid.NewGuid(),
                ProductId = productToBuy.ProductId,
                Price = productToBuy.Price
            };
            productToBuy.ItemsAvailable--;
            _dbContext.ProductCopies.Add(boughtItem);
            _dbContext.SaveChanges();

        }



    }
}