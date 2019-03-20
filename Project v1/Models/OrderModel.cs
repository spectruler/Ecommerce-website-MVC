using Project_v1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_v1.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string Province { get; set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string Email { get; set; }
        public virtual Account Account { get; set; }

        public IEnumerable<ItemOrderModel> OrderDetails { get; set; }
    }
}