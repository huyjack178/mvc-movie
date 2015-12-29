using System.Collections.Generic;

namespace MvcMovie.Controllers
{
    public interface IDataHandler
    {
        IEnumerable<object> GetAll();

        object Get(string id);

        void Create(object obj);

        void Update(object obj);

        void Delete(string id);
    }
}