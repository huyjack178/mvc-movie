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
            ClientUser user = (ClientUser)UserData.Get(userName);

            if (user != null)
            {
                return View(UserData.Get(userName));
            }

            return HttpNotFound();
        }
    }
}