using Assignment4.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Assignment4
{
    internal class Program
    {
        //test
        static void Main(string[] args)
        {
            var dataService = new DataService();
            
            var pp = dataService.GetProduct(2);
            Console.WriteLine("Her er en tom Category :) " + pp);

            /*
            foreach (var p in dataService.GetProducts())
            {
                Console.WriteLine(p);
            }*/
        }


       /* static void FirstTake()
        {
            var ctx = new NorthwindContext();

            var categories = ctx.Categories;

            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }

            var cat = new Category
            {
                Id = 101,
                Name = "fdfjslfdjfk",
                Description = "blah blah"
            };

            ctx.Categories.Add(cat);
            ctx.SaveChanges();
            
            //var test = ctx.Categories.Find(101);

            Console.WriteLine("hey");
            
            cat.Name = "New name";
            ctx.SaveChanges();

            ctx.Categories.Remove(ctx.Categories.Find(101));
            ctx.SaveChanges();

            var products = ctx.Products.Include(x => x.Category);
            
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }*/
    }
}