using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    [Table("PurchaseItemHistory")]
    public class PurchaseItemHistory
    {
        [Key]
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int PurchaseCount { get; set; }


    }
}