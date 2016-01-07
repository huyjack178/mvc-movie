using MvcMovie.Models;
using MvcMovie.Models.DataHandler;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class ValidationUserController : Controller
    {
        private UserDataHandler userData = new UserDataHandler();

        public JsonResult IsExistedUser(string userName)
        {
            User user = (User)userData.Get(userName);
            if (user == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json("User name already existed. Please try another one", JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsExistedEmail(string email)
        {
            if (string.IsNullOrEmpty(userData.GetEmail(email)))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json("Email already existed.", JsonRequestBehavior.AllowGet);
        }
    }
}