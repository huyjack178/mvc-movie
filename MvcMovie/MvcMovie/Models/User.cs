using MvcMovie.Models.DataHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Models
{
    public class User
    {
        public enum UserRoles
        {
            admin = 1,
            normal = 2
        }

        public int UserId { get; set; }

        [Display(Name = "User Name")]
        [StringLength(20)]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int Role { get; set; }
        
        public IEnumerable<SelectListItem> Roles = (from item in new UserDataHandler().GetRoles()
                                                    select new SelectListItem
                                                    {
                                                        Value = item.RoleId.ToString(),
                                                        Text = item.RoleName
                                                    });
    }
}