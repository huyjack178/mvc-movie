using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class NormalUser : User
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Register Date")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime RegDate { get; set; }
    }
}