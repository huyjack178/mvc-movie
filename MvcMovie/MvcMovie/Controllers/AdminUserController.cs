using MvcMovie.Models;
using System.Collections.Generic;
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
                return RedirectToAction("Logout", "AdminUser");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

            ModelState.AddModelError("invalid_msg", "Invalid UserName or Password");
            return View(user);
        }

        private bool IsAdminUser(User user)
        {
            User userData = (User)UserData.Get(user.UserName);

            if (userData != null)
            {
                if (userData.Role == (int)UserRole.RoleType.admin)
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

        [Authorize(Roles = "1")]
        [HttpGet]
        public ActionResult Index()
        {
            SetViewBagUserRoles();
            return View(UserData.GetAll());
        }

        private void SetViewBagUserRoles()
        {
            ViewBag.UserRole = new SelectList(UserData.GetRoles());
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public ActionResult Detail(string userName)
        {
            ClientUser user = (ClientUser)UserData.Get(userName);

            if (user != null)
            {
                return View(UserData.Get(userName));
            }

            return HttpNotFound();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterUser user)
        {
            if (ModelState.IsValid)
            {
                UserData.Create(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public ActionResult Edit(string userName)
        {
            User user = (User)UserData.Get(userName);
            if (user != null)
            {
                return View(user);
            }

            return HttpNotFound();
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientUser user)
        {
            if (ModelState.IsValid)
            {
                UserData.Update(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        public ActionResult Delete(string userName)
        {
            User user = (User)UserData.Get(userName);
            if (user != null)
            {
                return View(user);
            }

            return HttpNotFound();
        }

        [Authorize(Roles = "1")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string userName)
        {
            UserData.Delete(userName);

            return RedirectToAction("Index");
        }

        public ActionResult Search(string userName)
        {
            var users = (IEnumerable<ClientUser>)UserData.GetAll();

            users = UserData.FilterUserWithUserName(users, userName);

            return View("Index", users);
        }
    }
}