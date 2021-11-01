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
        //Orders
        public Order GetOrder(int orderId);
        public IList<Order> GetOrderByShippingName(string shippingName);
        public IList<Order> GetOrders();

        //OrderDetails
        public IList<OrderDetails> GetOrderDetailsByOrderId(int orderId);
        public IList<OrderDetails> GetOrderDetailsByProductId(int productId);

        //Products
        Product GetProduct(int productID);
        public IList<Product> GetProductByName(string input);
        IList<Product> GetProducts();
        public IList<Product> GetProductByCategory(int categoryId);

        //Categories
        Category GetCategory(int categoryId);
        IList<Category> GetCategories();
        bool CreateCategory(Category category);
       
        public bool DeleteCategory(int categoryId);

        public bool UpdateCategory(int categoryId, string updateName, string updateDescription);
        public bool UpdateCategory(Category cat);
       
        
        
        
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

        public Category CreateCategory(String newName, String newDesc)
        {
            var ctx = new NorthwindContext();

            Category category = new Category();
            category.Id = ctx.Categories.Max(x => x.Id) + 1;
            category.Name = newName;
            category.Description = newDesc;
            
            ctx.Add(category);
            ctx.SaveChanges();

            return category;
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
            try
            {
                ctx.Categories.Remove(ctx.Categories.Find(categoryId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            return ctx.SaveChanges() > 0;
        }
        public bool UpdateCategory(int categoryId, string updateName, string updateDescription)
        {
            var ctx = new NorthwindContext();
            Category cat = ctx.Categories.Find(categoryId);
            if (cat != null) {
                cat.Name = updateName;
                cat.Description = updateDescription;
            }
            
            return ctx.SaveChanges() > 0;
        }

        public bool UpdateCategory(Category cat)
        {
            var ctx = new NorthwindContext();
            Category temp = ctx.Categories.Find(cat.Id);

            temp.Name = cat.Name;
            temp.Description = cat.Description;
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

            // Product result = ctx.Products.Find(productId);
            Product result = ctx.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == productId);
            //ctx.Products.Include(x => x.Category);

            return result;
        }
        public IList<Product> GetProductByCategory(int categoryId)
        {
            var ctx = new NorthwindContext();

            var products = ctx.Products
                       .Include(x => x.Category)
                       .Where(p => p.CategoryId == categoryId)
                       .ToList()
                       ;

            return products;
        }


        public IList<Product> GetProductByName(string input)
        {
            var ctx = new NorthwindContext();

            var products = ctx.Products
                       .Include(x => x.Category)
                       .Where(p => p.Name.Contains(input))
                       .ToList();

            return products;
        }


        public IList<Order> GetOrders()
        {
            var ctx = new NorthwindContext();
            ctx.Orders.Include(x => x.OrderDetails);
            return ctx.Orders.ToList();
        }


        public Order GetOrder(int orderId)
        {
            var ctx = new NorthwindContext();
            Order result = ctx.Orders
                           .Include(x => x.OrderDetails)
                           .ThenInclude(p => p.Product)
                           .ThenInclude(c => c.Category)
                           .FirstOrDefault(x => x.Id == orderId);

            return result;
        }

   
        public IList<Order> GetOrderByShippingName(string shippingName)
        {
            var ctx = new NorthwindContext();
            var order = ctx.Orders
                       .Include(x => x.OrderDetails)
                       .ThenInclude(p => p.Product)
                       .ThenInclude(c => c.Category)
                       .Where(p => p.ShipName == shippingName)
                       .ToList();
            return order;
        }
        
        
        public IList<OrderDetails> GetOrderDetailsByOrderId(int orderId)
        {
            var ctx = new NorthwindContext();

            var orderDetails = ctx.OrderDetails
                       .Include(x => x.Product)
                       .ThenInclude(c => c.Category)
                       .Where(o => o.OrderId == orderId)
                       .ToList();

            return orderDetails;
        }
        
        
        public IList<OrderDetails> GetOrderDetailsByProductId(int productId)
        {
            var ctx = new NorthwindContext();
            var orderDetails = ctx.OrderDetails
                       .Include(x => x.Product)
                       .ThenInclude(c => c.Category)
                       .Include(o => o.Order)
                       .Where(p => p.Product.Id == productId)
                       .ToList();
            return orderDetails;
        }
    }
}

