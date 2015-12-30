﻿using Fanex.Data;
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
            using (IObjectDb db = new ObjectDb("User_GetUsers"))
            {
                return (IEnumerable<NormalUser>)db.Query<NormalUser>();
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

                List<NormalUser> users = (List<NormalUser>)db.Query<NormalUser>(param);

                return users.First<NormalUser>();
            }
        }

        public void Create(object obj)
        {
            using (IObjectDb db = new ObjectDb("User_CreateUser"))
            {
                NormalUser user = (NormalUser)obj;
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
                NormalUser user = (NormalUser)obj;
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
    }
}