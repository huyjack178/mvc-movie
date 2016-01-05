using Fanex.Data;
using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidateAntiForgeryTokenOnAllPosts : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            //  Only validate POSTs
            if (request.HttpMethod == WebRequestMethods.Http.Post)
            {
                //  Ajax POSTs and normal form posts have to be treated differently when it comes
                //  to validating the AntiForgeryToken
                if (request.IsAjaxRequest())
                {
                    var antiForgeryCookie = request.Cookies[AntiForgeryConfig.CookieName];

                    var cookieValue = antiForgeryCookie != null
                        ? antiForgeryCookie.Value
                        : null;

                    AntiForgery.Validate(cookieValue, request.Headers["__RequestVerificationToken"]);
                }
                else
                {
                    new ValidateAntiForgeryTokenAttribute()
                        .OnAuthorization(filterContext);
                }
            }
        }
    }

    public class MovieController : Controller
    {
        private MovieDataHandler movieDataHandler = new MovieDataHandler();

        public ActionResult IndexUser()
        {
            SetViewBagData();
            return View(movieDataHandler.GetAll());
        }

        // GET: /Movie/
        [Authorize(Roles = "1")]
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                SetViewBagData();
                return View(movieDataHandler.GetAll());
            }
            else
            {
                return RedirectToAction("Login", "AdminUser");
            }
        }

        public void SetViewBagData()
        {
            ViewBag.movieGenre = new SelectList(GetGerneList());
        }

        private List<string> GetGerneList()
        {
            List<string> genreList = new List<string>();
            IEnumerable<Movie> movies = (IEnumerable<Movie>)movieDataHandler.GetAll();

            foreach (Movie movie in movies)
            {
                genreList.Add(movie.Genre);
            }

            genreList = genreList.Distinct<string>().ToList();
            genreList.Sort();

            return genreList;
        }

        // GET: /Movie/Details/5
        public ActionResult Details(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return View((Movie)movieDataHandler.Get(id));
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: /Movie/Create
        [Authorize(Roles = "1")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                ImageHandler.CreateImageFrom(movie);
                movieDataHandler.Create(movie);
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: /Movie/Edit/5
        [Authorize(Roles = "1")]
        public ActionResult Edit(string id)
        {
            using (IObjectDb db = new ObjectDb("Movie_GetMovie"))
            {
                Movie movie = (Movie)movieDataHandler.Get(id);

                if (movie == null)
                {
                    return HttpNotFound();
                }

                return View(movie);
            }
        }

        [Authorize(Roles = "1")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                movieDataHandler.Update(movie);
                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize(Roles = "1")]
        public ActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Movie movie = (Movie)movieDataHandler.Get(id);

                if (movie == null)
                {
                    return HttpNotFound();
                }

                return View(movie);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: /Movie/Delete/5
        [Authorize(Roles = "1")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            movieDataHandler.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult SearchIndex(string movieGenre, string searchString)
        {
            IEnumerable<Movie> movies = (IEnumerable<Movie>)movieDataHandler.GetAll();

            movies = movieDataHandler.FilterMoviesWithGenre(movies, movieGenre);
            movies = movieDataHandler.FilterMoviesWithTitle(movies, searchString.ToLower());

            SetViewBagData();

            return View("Index", movies);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}