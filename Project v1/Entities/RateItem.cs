using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    [Table("RateItem")]
    public class RateItem
    {
        [Key, Column(Order=1)]
        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public double rate { get; set; }
    }
}