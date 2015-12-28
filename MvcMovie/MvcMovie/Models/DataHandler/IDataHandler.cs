using System.Collections.Generic;

namespace MvcMovie.Controllers
{
    public interface IDataHandler
    {
        IEnumerable<object> GetAll();

        object Get(int id);

        void Create(object obj);

        void Update(object obj);

        void Delete(int id);
    }
}