﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment4.Domain
{
    public class OrderDetails
    {
        public string orderId { get; set; }
        public string productId { get; set; }
        public string unitPrice { get; set; }
        public string quantity { get; set; }
        public string discount { get; set; }
    }
}
