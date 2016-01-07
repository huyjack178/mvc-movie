using Fanex.Data;
using MvcMovie.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcMovie.Controllers
{
    public class MovieDataHandler : IDataHandler
    {
        public MovieDataHandler()
        {
        }

        public IEnumerable<object> GetAll()
        {
            using (IObjectDb db = new ObjectDb("Movie_GetMovies"))
            {
                IEnumerable<Movie> movies = (IEnumerable<Movie>)db.Query<Movie>();

                return movies;
            }
        }

        public object Get(string id)
        {
            using (IObjectDb db = new ObjectDb("Movie_GetMovie"))
            {
                var param = new
                {
                    Id = id
                };

                List<Movie> movies = (List<Movie>)db.Query<Movie>(param);

                return movies.FirstOrDefault();
            }
        }

        public void Create(object obj)
        {
            using (IObjectDb db = new ObjectDb("Movie_CreateMovie"))
            {
                Movie movie = (Movie)obj;
                db.ExecuteNonQuery(movie);
            }
        }

        public void Update(object obj)
        {
            using (IObjectDb db = new ObjectDb("Movie_UpdateMovie"))
            {
                Movie movie = (Movie)obj;
                db.ExecuteNonQuery(movie);
            }
        }

        public void Delete(string id)
        {
            using (IObjectDb db = new ObjectDb("Movie_DeleteMovie"))
            {
                var param = new
                {
                    Id = id
                };

                db.ExecuteNonQuery(param);
            }
        }

        public IEnumerable<Movie> FilterMoviesWithGenre(IEnumerable<Movie> movies, string genre)
        {
            if (!string.IsNullOrEmpty(genre))
            {
                movies = movies.Where(movie => movie.Genre == genre);
            }

            return movies;
        }

        public IEnumerable<Movie> FilterMoviesWithTitle(IEnumerable<Movie> movies, string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                movies = movies.Where(movie => movie.Title.ToLower().Contains(title));
            }

            return movies;
        }
    }
}