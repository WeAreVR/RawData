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
        public OrderDetails GetOrderDetailsByOrderId(int orderId);
        public IList<OrderDetails> GetOrderDetailsByProductId(int productId);

        //Products
        Product GetProduct(int productID);
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

        public bool UpdateCategory(Category cat)
        {
            var ctx = new NorthwindContext();
            Category temp = ctx.Categories.Find(cat.Id);

            temp.Name = cat.Name;
            temp.Description = cat.Description;
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
            ctx.Products.Include(x => x.Category);

            return result;
        }
        public IList<Product> GetProductByCategory(int categoryId)
        {
            var ctx = new NorthwindContext();
            /*var products = from p in ctx.Products
                           where p.CategoryId == categoryId
                           select p;*/

            var products = ctx.Products
                       .Where(p => p.CategoryId == categoryId)
                       .Include(x => x.Category)
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
            Order result = ctx.Orders.Find(orderId);
            ctx.Orders.Include(x => x.OrderDetails);
            return result;
        }
   
        public IList<Order> GetOrderByShippingName(string shippingName)
        {
            var ctx = new NorthwindContext();
            var order = ctx.Orders
                       .Where(p => p.ShipName == shippingName)
                       .Include(x => x.OrderDetails)
                       .ToList();
            return order;
        }
        
        
        //Vi mangler at få product name har kun ID i orderdetails
        public OrderDetails GetOrderDetailsByOrderId(int orderId)
        {
            var ctx = new NorthwindContext();
            OrderDetails result = ctx.OrderDetails.Find(orderId);
            ctx.OrderDetails.Include(x => x.Product);

            return result;
        }
        
        
        public IList<OrderDetails> GetOrderDetailsByProductId(int productId)
        {
            var ctx = new NorthwindContext();
            var orderDetails = ctx.OrderDetails
                       .Where(p => p.Product.Id == productId)
                       .Include(x => x.Product)
                       .ToList();
            return orderDetails;
        }
        
        /*

        public IList<Product> GetProductByCategory(int categoryId)
        {
            var ctx = new NorthwindContext();
            var products = from p in ctx.Products
                           where p.CategoryId == categoryId
                           select p;
           
            return products.ToList();
        }*/
    }
}

