using Project_v1.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_v1.Areas.Admin.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [MaxLength(256)]
        [Required(ErrorMessage ="Product Name is Required")]
        public string Name { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        [Required(ErrorMessage ="Image URL for Product is Required")]
        [Display(Name="Image URL")]
        public string ImageUrl { get; set; }
        // further details if needed


        public int? ProductTypeId { get; set; } 

        [Display(Name="Product Type")]
        public ICollection<ProductType> ProductTypes { get; set; }

        public string ProductType
        {
            get
            {
            return ProductTypes == null || ProductTypes.Count.Equals(0) ?
            String.Empty : ProductTypes.First(pt => pt.Id.Equals(ProductTypeId)).Name;
            }
    }
    }
}