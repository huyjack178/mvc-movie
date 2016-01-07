using MvcMovie.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class MovieController : Controller
    {
        private MovieDataHandler movieDataHandler = new MovieDataHandler();

        // GET: /Movie/
        [Authorize(Roles = "1")]
        public virtual ActionResult Index()
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
        public virtual ActionResult Details(string id)
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
                movieDataHandler.Create(movie);
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: /Movie/Edit/5
        [Authorize(Roles = "1")]
        public ActionResult Edit(string id)
        {
            Movie movie = (Movie)movieDataHandler.Get(id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
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

                return null;
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

        public virtual ActionResult Search(string movieGenre, string searchString)
        {
            IEnumerable<Movie> movies = (IEnumerable<Movie>)movieDataHandler.GetAll();

            movies = movieDataHandler.FilterMoviesWithGenre(movies, movieGenre);
            movies = movieDataHandler.FilterMoviesWithTitle(movies, searchString.ToLower());

            SetViewBagData();

            return View("Index", movies);
        }
    }
}