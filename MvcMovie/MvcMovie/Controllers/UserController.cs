using MvcMovie.Models;
using MvcMovie.Models.DataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class UserController : Controller
    {

        private UserDataHandler userDataHandler = new UserDataHandler();

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                User user = (User)userDataHandler.Get(userName);

                if (user.Password == password && user.Role == (int)Models.User.UserRoles.Admin)
                {
                    Session["UserName"] = user.UserName;
                    return RedirectToAction("Index", "Movie");
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public void Edit(int id)
        {

        }

        public void Delete(int id)
        {

        }
    }
}
