using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    [Table("Product")]
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [MaxLength(256)]
        [Required(ErrorMessage ="Product Name is Required")]
        public string Name { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        [Display(Name="Image URL")]
        [Required(ErrorMessage ="Image URL for Product is Required")]
        public string ImageUrl { get; set; }
        // further details if needed

        
        public int? ProductTypeId { get; set; } //can be used as sub-category in which this product comes
        public virtual ProductType ProductType { get; set; }
    }
}