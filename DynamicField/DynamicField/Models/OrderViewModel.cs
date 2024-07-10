using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicField.Models
{
    public class OrderViewModel
    {
        public Customer customer { get; set; }
        public List<OrderItem> orderItems { get; set; }
    }
}