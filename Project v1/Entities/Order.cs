using Project_v1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    [Table("Order")]
    public class Order
    {
        //Add constraint here for starting and ending format
        [Key,Column(Order=1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string Province { get;set; }
        public string Phone { get; set; }
        public decimal Total { get; set; }
        public System.DateTime OrderDate { get; set; }
        
        public string Email{ get; set; }
        public virtual Account Account { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}