using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class User
    {
        public enum UserRoles
        {
            Admin = 1,
            Normal = 2
        }

        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int Role { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime RegDate { get; set; }
    }
}