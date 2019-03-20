using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    [Table("Category")]
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(256)]
        [Required(ErrorMessage ="Category Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Image URl is Required for category")]
        [Display(Name="Image Url")]
        public string ImageUrl { get; set; }


    }
}