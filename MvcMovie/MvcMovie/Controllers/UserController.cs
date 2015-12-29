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
    public class UserController : Controller
    {

        private UserDataHandler userDataHandler = new UserDataHandler();

        //
        // GET: /User/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                User userData = (User)userDataHandler.Get(user.UserName);

                if (userData.Password == user.Password)
                {
                    FormsAuthentication.SetAuthCookie(userData.UserName, false);
                    if (userData.Role == (int)Models.User.UserRoles.Admin)
                    {
                        return RedirectToAction("Index", "Movie");
                    }
                    else
                    {
                        return RedirectToAction("IndexUser", "Movie");
                    }
                }
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


    }
}
