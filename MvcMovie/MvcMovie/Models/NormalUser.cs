using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class NormalUser: User
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime RegDate { get; set; }
    }
}