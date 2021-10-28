using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Assignment4.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [NotMapped]
        public String CategoryName { get; set; }

        public override string ToString()
        {
            return $"Id = {Id}, Name = {Name}, Category = {Category}";
        }
    }
}