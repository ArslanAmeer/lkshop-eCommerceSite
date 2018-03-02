using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FYProject1Classes.UserMgmt
{
    public class UserHandler
    {
        private DBContextClass db = new DBContextClass();
        public List<User> GetUsers()
        {
            using (db)
            {
                return (from u in db.Users
                        .Include("Role")
                        .Include("CityId.Country")
                        .Include("UserImage")
                        select u).ToList();
            }
        }

        public List<User> GetUsersByName()
        {
            using (db)
            {
                return (from u in db.Users.OrderBy(u => u.FullName)
                        .Include("Role")
                        .Include("CityId.Country")
                        .Include("UserImage")
                        select u).ToList();
            }
        }

        public User GetUserByEmail(string email)
        {
            using (db)
            {
                return (from u in db.Users
                        where u.Email == email
                        select u).FirstOrDefault();
            }
        }

        public User GetUser(int? id)
        {
            using (db)
            {
                return (from u in db.Users
                        .Include("Role")
                        .Include("CityId.Country")
                        .Include("UserImage")
                        where u.Id == id
                        select u).FirstOrDefault();
            }
        }

        public User GetUser(string LoginId, string Password)
        {
            using (db)
            {
                return (from u in db.Users
                        .Include("Role")
                        .Include("CityId.Country")
                        .Include("UserImage")
                        where u.LoginID.Equals(LoginId) && u.Password.Equals(Password)
                        select u).FirstOrDefault();
            }
        }

        public List<Role> GetRoles()
        {
            using (db)
            {
                return (from r in db.Roles select r).ToList();
            }
        }
        public Role GetRoles(int id)
        {
            using (db)
            {
                return (from r in db.Roles
                        where r.Id == id
                        select r).FirstOrDefault();
            }
        }

        public void Adduser(User user)
        {
            using (db)
            {
                db.Entry(user.Role).State = EntityState.Unchanged;
                db.Entry(user.CityId).State = EntityState.Unchanged;
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            using (db)
            {
                //if any error occured just Uncomment this code

                //User u = db.Users.Find(id);
                db.Users.Remove(GetUser(id));
                db.SaveChanges();
            }
        }

        public void UpdateUser(User user)
        {
            using (db)
            {                
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public int GetUserCount()
        {
            using (db)
            {
                return (from c in db.Users select c).Count();
            }
        }
    }
}
