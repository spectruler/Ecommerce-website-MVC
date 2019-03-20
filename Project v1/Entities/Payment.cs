using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    [Table("Payment")]
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PayId { get; set; }

        
        public int? CardNumber { get; set; }

        public double Amount { get; set; } // current account existing amount

        public string PayType { get; set; }

        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }

        public string Email { get; set; }
        public virtual Account Account { get; set; }

        public DateTime PayDate { get; set; }

        //rest of 
        
    }
}