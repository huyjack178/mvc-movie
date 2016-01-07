using MvcMovie.Models;
using MvcMovie.Models.DataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class ValidationUserController : Controller
    {
        private UserDataHandler userData = new UserDataHandler();

        public JsonResult IsExistedUser(string UserName)
        {
            User user = (User)userData.Get(UserName);
            if (user == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json("User name already existed. Please try another one", JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsExistedEmail(string Email)
        {
            if (string.IsNullOrEmpty(userData.GetEmail(Email)))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json("Email already existed.", JsonRequestBehavior.AllowGet);
        }

    }
}
