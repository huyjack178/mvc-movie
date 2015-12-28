using Fanex.Data;
using MvcMovie.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class MovieController : Controller
    {
        private MovieDataHandler movieDataHandler = new MovieDataHandler();

        // GET: /Movie/
        public ActionResult Index()
        {
            SetViewBagData();

            return View(movieDataHandler.GetAll());
        }

        private void SetViewBagData()
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
        public ActionResult Details(int id = 0)
        {
            if (id >= 0)
            {
                return View((Movie)movieDataHandler.Get(id));
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // POST: /Movie/Create
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
        public ActionResult Edit(int id = 0)
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

        // POST: /Movie/Edit/5
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

        // GET: /Movie/Delete/5
        public ActionResult Delete(int id = 0)
        {
            if (id >= 0)
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

        // [ValidateAntiForgeryToken]
        // POST: /Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
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