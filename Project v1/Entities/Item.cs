using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    [Table("Item")]
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(256)]
        [Required(ErrorMessage ="Item Name is Required")]
        public string Name { get; set; }

        [MaxLength(2048)]
        public string Description { get; set; }

        [Display(Name="Image Url")]
        [Required(ErrorMessage ="Image Url of Item is Required")]
        public string ImageUrl { get; set; }

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Range(0,double.MaxValue)]
        public double Price { get; set; }

        public bool Availability { get; set; }
        [Range(0,int.MaxValue)]
        public int Quantity { get; set; }
    }
}