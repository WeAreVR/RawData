using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment4.Domain
{
    class Order
    {
        public int orderId { get; set; }
        public string customerId { get; set; }
        public int employeeId { get; set; }
        public string orderDate { get; set; }
        public string requiredDate { get; set; }
        public string shippedDate { get; set; }
        public int freight { get; set; }
        public string shipName { get; set; }
        public string shipAddress { get; set; }
        public string shipCity { get; set; }
        public string shipCountry { get; set; }
        public string shipPostalCode { get; set; }


    }
}
