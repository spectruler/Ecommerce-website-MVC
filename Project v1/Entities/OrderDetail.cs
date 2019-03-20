using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    [Table("OrderDetail")]
    public class OrderDetail
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key,Column(Order=1)]
        public int OrderDetailId { get; set; }

        public int? OrderId { get; set; }
        [Key,Column(Order=2)]
        public int? ItemId { get; set; }

        [Range(1,Int32.MaxValue)]
        [DefaultValue(1)]
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}