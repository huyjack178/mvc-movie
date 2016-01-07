using MvcMovie.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class MovieClientController : MovieController
    {
        private MovieDataHandler movieDataHandler = new MovieDataHandler();

        [AllowAnonymous]
        public override ActionResult Index()
        {
            SetViewBagData();
            return View(movieDataHandler.GetAll());
        }

        public override ActionResult Details(string id)
        {
            return base.Details(id);
        }

        public override ActionResult Search(string movieGenre, string searchString)
        {
            IEnumerable<Movie> movies = (IEnumerable<Movie>)movieDataHandler.GetAll();

            movies = movieDataHandler.FilterMoviesWithGenre(movies, movieGenre);
            movies = movieDataHandler.FilterMoviesWithTitle(movies, searchString.ToLower());

            SetViewBagData();

            return View("Search", movies);
        }
    }
}