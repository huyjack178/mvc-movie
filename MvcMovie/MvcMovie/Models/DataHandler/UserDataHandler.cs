using Fanex.Data;
using MvcMovie.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models.DataHandler
{
    public class UserDataHandler : IDataHandler
    {
        public IEnumerable<object> GetAll()
        {
            throw new NotImplementedException();
        }

        public object Get(string id)
        {
            using (IObjectDb db = new ObjectDb("User_GetUser"))
            {
                var param = new
                {
                    UserName = id
                };

                List<User> users = (List<User>)db.Query<User>(param);

                return users.First<User>();
            }
        }

        public void Create(object obj)
        {
            throw new NotImplementedException();
        }

        public void Update(object obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}