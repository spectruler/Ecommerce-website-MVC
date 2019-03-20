using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_v1.Models
{
    public class ItemOrderModel
    {
        public int ItemId { get; set; }
        public string ProductName { get; set; }
        public string ItemName { get; set; }
        public int? ProductId { get; set; }
        public int ItemQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public bool Availablity { get; set; }
        public string ImageUrl { get; set; }
        public double UnitPrice { get; set; }
        public string ItemDescription { get; set; }
    }
}