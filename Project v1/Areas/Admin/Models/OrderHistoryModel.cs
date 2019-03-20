using Project_v1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_v1.Areas.Admin.Models
{
    public class OrderHistoryModel
    {

        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }


        public int? PayId { get; set; }
        public virtual Payment Payment { get; set; }


        public int? ItemId { get; set; }
        public virtual Item Item { get; set; }

        public string Email { get; set; }
        public virtual Account Account { get; set; }

        public double UnitPrice { get; set; }

        public int ItemQuantity { get; set; }

        public string ItemName { get; set; }
        
        public DateTime date { get; set; }
    }
}