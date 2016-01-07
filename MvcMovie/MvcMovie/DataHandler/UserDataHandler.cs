using Fanex.Data;
using MvcMovie.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace MvcMovie.Models.DataHandler
{
    public class UserDataHandler : IDataHandler
    {
        public IEnumerable<object> GetAll()
        {
            using (IObjectDb db = new ObjectDb("User_GetUsers"))
            {
                return (IEnumerable<ClientUser>)db.Query<ClientUser>();
            }
        }

        public object Get(string id)
        {
            using (IObjectDb db = new ObjectDb("User_GetUser"))
            {
                var param = new
                {
                    UserName = id
                };

                List<ClientUser> users = (List<ClientUser>)db.Query<ClientUser>(param);

                return users.FirstOrDefault();
            }
        }

        public void Create(object obj)
        {
            using (IObjectDb db = new ObjectDb("User_CreateUser"))
            {
                ClientUser user = (ClientUser)obj;
                user.RegDate = System.DateTime.Now;

                var param = new
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Role = user.Role,
                    Email = user.Email,
                    RegDate = user.RegDate
                };

                db.ExecuteNonQuery(param);
            }
        }

        public void Update(object obj)
        {
            using (IObjectDb db = new ObjectDb("User_UpdateUser"))
            {
                ClientUser user = (ClientUser)obj;
                var param = new
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Role = user.Role,
                    Email = user.Email,
                };

                db.ExecuteNonQuery(param);
            }
        }

        public void Delete(string id)
        {
            using (IObjectDb db = new ObjectDb("User_DeleteUser"))
            {
                var param = new
                {
                    UserName = id
                };

                db.ExecuteNonQuery(param);
            }
        }

        public IEnumerable<UserRole> GetRoles()
        {
            using (IObjectDb db = new ObjectDb("UserRole_GetUserRoles"))
            {
                return (IEnumerable<UserRole>)db.Query<UserRole>();
            }
        }

        public string GetRole(int id)
        {
            using (IObjectDb db = new ObjectDb("UserRole_GetUserRole"))
            {
                return db.Query<string>().FirstOrDefault();
            }
        }

        public string GetEmail(string email)
        {
            using (IObjectDb db = new ObjectDb("User_GetEmail"))
            {
                List<string> emails = (List<string>)db.Query<string>(new { Email = email });
                return emails.FirstOrDefault();
            }
        }

        public IEnumerable<ClientUser> FilterUserWithUserName(IEnumerable<ClientUser> users, string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                users = users.Where(user => user.UserName.Contains(userName));
            }

            return users;
        }
    }
}