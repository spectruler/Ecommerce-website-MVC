using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    [Table("ProductType")]
    public class ProductType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(256)]
        [Required(ErrorMessage ="Name For Product Type is Required")]
        public string Name { get; set; }
        
        public int? CategoryId { get; set; } 
        public virtual Category Category { get; set; }

        [Required(ErrorMessage = ("Image URL is Required for Product Type"))]
        [Display(Name="Image URL")]
        public string ImageUrl { get; set; }
    }
}