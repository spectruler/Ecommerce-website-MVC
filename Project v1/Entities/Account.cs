using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project_v1.Entities
{
    [Table("Account")]
    public class Account
    {
        
        [Required(ErrorMessage ="First Name is required")]
        [StringLength(16,ErrorMessage ="The {0} must be at least {1} characters long.",MinimumLength =2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(16, ErrorMessage = "The {0} must be at least {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        //Add password attribute and email 
        [Key]
        [EmailAddress]
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [StringLength(100)]
        [Display(Name ="Password")]
        public string Password { get; set; }

    }
}