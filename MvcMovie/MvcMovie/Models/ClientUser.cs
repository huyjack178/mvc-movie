using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class ClientUser : User
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public virtual string Email { get; set; }

        [Display(Name = "Register Date")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime RegDate { get; set; }
    }
}