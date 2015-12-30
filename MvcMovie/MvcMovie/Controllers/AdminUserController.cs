using MvcMovie.Models;
using MvcMovie.Models.DataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcMovie.Controllers
{
    public class AdminUserController : UserController
    {
        

        [HttpGet]
        public override ActionResult Login()
        {
            
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Movie");
            }
            
            return View();
        }

        [HttpPost]
        public override ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                if (IsValidUser(user))
                {
                    if (IsAdminUser(user))
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, false);
                        return RedirectToAction("Index", "Movie");
                    }
                }
            }

            return View(user);
        }

        private bool IsAdminUser(User user)
        {
            User userData = (User)userDataHandler.Get(user.UserName);

            if (userData != null)
            {
                if (userData.Role == (int)Models.User.UserRoles.Admin)
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

        [HttpGet]
        public ActionResult Index()
        {
            return View(userDataHandler.GetAll());
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NormalUser user)
        {
            if (ModelState.IsValid)
            {
                userDataHandler.Create(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(string userName)
        {
            User user = (User)userDataHandler.Get(userName);
            if (user != null)
            {
                return View(user);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NormalUser user)
        {
            if (ModelState.IsValid)
            {
                userDataHandler.Update(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Delete(string userName)
        {
            User user = (User)userDataHandler.Get(userName);
            if (user != null)
            {
                return View(user);
            }

            return HttpNotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string userName)
        {
            userDataHandler.Delete(userName);

            return RedirectToAction("Index");
        }
    }
}
