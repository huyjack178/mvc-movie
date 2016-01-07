using MvcMovie.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcMovie.Controllers
{
    public class ClientUserController : UserController
    {
        [HttpGet]
        public override ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Login(User user)
        {
            if (IsValidUser(user))
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound();
        }

        public override ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "MovieClient");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(ClientUser user)
        {
            if (ModelState.IsValid)
            {
                user.Role = (int)UserRole.RoleType.normal;
                UserData.Create(user);
                return RedirectToAction("Index", "MovieClient");
            }

            return View(user);
        }

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
    }
}