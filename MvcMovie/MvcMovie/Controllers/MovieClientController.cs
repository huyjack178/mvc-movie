using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class MovieClientController : MovieController
    {
        private MovieDataHandler movieDataHandler = new MovieDataHandler();
        public override ActionResult Index()
        {
            return base.Index();
        }

        public override ActionResult Details(string id)
        {
            return base.Details(id);
        }

        public override ActionResult Search(string movieGenre, string searchString)
        {
            return base.Search(movieGenre, searchString);
        }
    }
}
