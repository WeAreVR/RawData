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

        IList<Product> GetProducts();
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

        public IList<Product> GetProducts()
        {
            var ctx = new NorthwindContext();
            return ctx.Products.ToList();
        }
    }
}

