using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment4.Domain;

namespace Assignment4
{
        public interface IDataService
    {
        IList<Category> GetCategories();
        bool CreateCategory(Category category);
        Category GetCategory(int categoryId);
        public bool DeleteCategory(int categoryId);

        public bool UpdateCategory(int categoryId, string updateName, string updateDescription);
        IList<Product> GetProducts();
        Product GetProduct(int productID);
        public IList<Product> GetProductByCategory(int categoryId);
    }

    public class DataService : IDataService
    {
        public bool CreateCategory(Category category)
        {
            var ctx = new NorthwindContext();
            category.Id = ctx.Categories.Max(x => x.Id) + 1;
            ctx.Add(category);
            return ctx.SaveChanges() > 0;
        }


        public IList<Category> GetCategories()
        {
            var ctx = new NorthwindContext();
            return ctx.Categories.ToList();
        }
        public Category GetCategory(int categoryId)
        {
            var ctx = new NorthwindContext();
            Category result = ctx.Categories.Find(categoryId);
            return result;
        }
        public bool DeleteCategory(int categoryId)
        {
            var ctx = new NorthwindContext();
            ctx.Categories.Remove(ctx.Categories.Find(categoryId));
            ctx.SaveChanges();
            return ctx.SaveChanges() > 0;
        }
        public bool UpdateCategory(int categoryId, string updateName, string updateDescription)
        {
            var ctx = new NorthwindContext();
            Category cat = ctx.Categories.Find(categoryId);

            cat.Name = updateName;
            cat.Description = updateDescription;
            ctx.SaveChanges();
            return ctx.SaveChanges() > 0;
        }

        public IList<Product> GetProducts()
        {
            var ctx = new NorthwindContext();
            return ctx.Products.ToList();
        }
        public Product GetProduct(int productId)
        {
            var ctx = new NorthwindContext();
            Product result = ctx.Products.Find(productId);
            return result;
        }
        public IList<Product> GetProductByCategory(int categoryId)
        {
            var ctx = new NorthwindContext();
            var products = from p in ctx.Products
                           where p.CategoryId == categoryId
                           select p;
            foreach (var product in products)
            {
                product.CategoryName = GetCategory(product.CategoryId).Name;
            }
            return products.ToList();
        }
        public orders GetOrder(int orderId)
        {
            var ctx = new NorthwindContext();
            Product result = ctx.Products.Find(orderId);
            return result;
        }
    }
}

