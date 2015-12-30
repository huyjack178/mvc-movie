using MvcMovie.Models;
using MvcMovie.Models.DataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcMovie.Controllers
{
    public abstract class UserController : Controller
    {
        protected UserDataHandler userDataHandler = new UserDataHandler();

        [HttpGet]
        public abstract ActionResult Login();

        [HttpPost]
        public abstract ActionResult Login(User user);
       
        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        protected bool IsValidUser(User user)
        {
            User userData = (User)userDataHandler.Get(user.UserName);

            if (userData != null)
            {
                if (userData.Password == user.Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

    }
}
