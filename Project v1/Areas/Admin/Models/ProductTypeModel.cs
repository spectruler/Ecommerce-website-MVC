using Project_v1.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_v1.Areas.Admin.Models
{
    public class ProductTypeModel
    {
        public int Id { get; set; }

        [MaxLength(256)]
        [Required(ErrorMessage = "Name For Product Type is Required")]
        public string Name { get; set; }


        public int? CategoryId { get; set; } //used for foreign key

        [Required(ErrorMessage = ("Image URL is Required for Product Type"))]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Display(Name = "Category")]
        public ICollection<Category> Categories { get; set; }

        public string Category
        {
            get
            {
                return Categories == null || Categories.Count.Equals(0) ?
                    String.Empty : Categories.First(pt => pt.Id.Equals(CategoryId)).Name;
            }
        }
    }
}