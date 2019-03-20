using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    [Table("OrderHistory")]
    public class OrderHistory
    {
        [Key, Column(Order = 1)]
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Key, Column(Order = 2)]
        public int? PayId { get; set; }
        public virtual Payment  Payment{get;set;}

        [Key,Column(Order=3)]
        [ForeignKey("Item")]
        public int? ItemId { get; set; }
        public virtual Item Item { get; set; }

        public string Email { get; set; }
        public virtual Account Account { get; set; }

        public double UnitPrice { get; set; }

        public int ItemQuantity { get; set; }
    }
}