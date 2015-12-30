using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcMovie.Controllers
{
    public class NormalUserController : UserController
    {
        [HttpGet]
        public override ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public override ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                if (IsValidUser(user))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("IndexUser", "Movie");
                }
            }

            return View(user);
        }

        public override ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("IndexUser", "Movie");
        }

        [HttpGet]
        public ActionResult Detail(string userName)
        {
            NormalUser user = (NormalUser)userDataHandler.Get(userName);

            if (user != null)
            {
                return View(userDataHandler.Get(userName));
            }

            return HttpNotFound();
        }
    }
}
