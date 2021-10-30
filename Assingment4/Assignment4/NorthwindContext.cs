using Microsoft.EntityFrameworkCore;
using System;
using Assignment4.Domain;

namespace Assignment4
{
    
    public class NorthwindContext : DbContext
    {
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
       /* public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseNpgsql("host=localhost;db=Northwind;uid=postgres;pwd=ctj66yjr");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");
            modelBuilder.Entity<Category>().Property(x => x.Description).HasColumnName("description");
          


            modelBuilder.Entity<Product>().ToTable("products");
            modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnName("productid");
            modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnName("productname");
            modelBuilder.Entity<Product>().Property(x => x.CategoryId).HasColumnName("categoryid");
           // modelBuilder.Entity<Product>().HasOne(q => q.Category).WithOne().IsRequired();



            /*
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<Order>().Property(x => x.orderId).HasColumnName("orderid");
            modelBuilder.Entity<Order>().Property(x => x.customerId).HasColumnName("customerid");
            modelBuilder.Entity<Order>().Property(x => x.employeeId).HasColumnName("employeeid");
            modelBuilder.Entity<Order>().Property(x => x.orderDate).HasColumnName("orderdate");
            modelBuilder.Entity<Order>().Property(x => x.requiredDate).HasColumnName("requireddate");
            modelBuilder.Entity<Order>().Property(x => x.shippedDate).HasColumnName("shippeddate");
            modelBuilder.Entity<Order>().Property(x => x.freight).HasColumnName("freight");
            modelBuilder.Entity<Order>().Property(x => x.shipName).HasColumnName("shipname");
            modelBuilder.Entity<Order>().Property(x => x.shipAddress).HasColumnName("shipaddress");
            modelBuilder.Entity<Order>().Property(x => x.shipCountry).HasColumnName("shipcountry");
            modelBuilder.Entity<Order>().Property(x => x.shipPostalCode).HasColumnName("shippostalcode");
            modelBuilder.Entity<Order>().Property(x => x.shipCity).HasColumnName("shipcity");

            modelBuilder.Entity<OrderDetails>().ToTable("orderdetails");
            modelBuilder.Entity<OrderDetails>().Property(x => x.orderId).HasColumnName("orderid");
            modelBuilder.Entity<OrderDetails>().Property(x => x.productId).HasColumnName("productid");
            modelBuilder.Entity<OrderDetails>().Property(x => x.unitPrice).HasColumnName("unitprice");
            modelBuilder.Entity<OrderDetails>().Property(x => x.quantity).HasColumnName("quantity");
            modelBuilder.Entity<OrderDetails>().Property(x => x.discount).HasColumnName("discount");
            */
        }
    }
}
