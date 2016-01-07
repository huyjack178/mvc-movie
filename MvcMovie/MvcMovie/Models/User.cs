using MvcMovie.Models.DataHandler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace MvcMovie.Models
{
    public class User
    {
        private IEnumerable<SelectListItem> roles = from item in new UserDataHandler().GetRoles()
                                                    select new SelectListItem
                                                    {
                                                        Value = item.RoleId.ToString(),
                                                        Text = item.RoleName
                                                    };

        public IEnumerable<SelectListItem> Roles
        {
            get { return roles; }
        }

        public int UserId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed.")]
        public virtual string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public int Role { get; set; }
    }
}