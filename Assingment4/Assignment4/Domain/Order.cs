using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment4.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Required { get; set; }
        public DateTime Shipped { get; set; }
        public int Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipCountry { get; set; }
        public string ShipPostalCode { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }


    }
}
