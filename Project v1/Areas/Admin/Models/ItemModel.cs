using Project_v1.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_v1.Areas.Admin.Models
{
    public class ItemModel
    {
        public int Id;

        [MaxLength(256)]
        [Required(ErrorMessage = ("Item Name is Required"))]
        public string Name { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        //should be +ve ________________________________________________________

        public int? ProductId { get; set; }

        /// <summary>
        /// Price should be positive hence check ------------------------------------------------------------
        /// </summary>
        [Range(0,Double.MaxValue)]
        public double Price { get; set; }

        // if not quantity should be must check ________________________________________________________________
        public bool Availability { get; set; }

        public int Quantity { get; set; }

        public ICollection<Product> Products { get; set; }

        public string Product
        {
            get
            {
                return Products == null || Products.Count.Equals(0) ?
                    String.Empty : Products.First(pt => pt.Id.Equals(ProductId)).Name;
            }
        }
    }
}