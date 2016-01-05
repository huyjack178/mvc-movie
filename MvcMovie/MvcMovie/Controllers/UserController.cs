using MvcMovie.Models;
using MvcMovie.Models.DataHandler;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcMovie.Controllers
{
    public abstract class UserController : Controller
    {
        private UserDataHandler userData = new UserDataHandler();

        public UserDataHandler UserData
        {
            get { return userData; }
        }

        [HttpGet]
        public abstract ActionResult Login();

        [HttpPost]
        public abstract ActionResult Login(User user);

        public virtual ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "AdminUser");
        }

        protected bool IsValidUser(User user)
        {
            User userData = (User)UserData.Get(user.UserName);

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