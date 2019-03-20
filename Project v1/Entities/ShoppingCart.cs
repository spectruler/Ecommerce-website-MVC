using Project_v1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CartId { get; set; }

        [Display(Name="Email Address")]
        public string Email { get; set; } // add constraint for valid email address
        public virtual Account Account { get; set; }
        
        //Add attribute for bills
        public int count;
        public System.DateTime DateCreated { get; set; }
        public int? ItemId { get; set; }
        public virtual Item Item { get; set; }

        public List<Item> Items { get; set; }
    }
}