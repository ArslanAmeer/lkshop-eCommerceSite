using FYProject1Classes.LocationMgmt;
using System;
using System.Collections.Generic;

namespace FYProject1Classes.UserMgmt
{
    public class User
    {
        public User()
        {
            UserImage = new List<UserImage>();
        }
        public int Id { get; set; }

        public string FullName { get; set; }

        public string LoginID { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public ICollection<UserImage> UserImage { get; set; }

        public string FullAddress { get; set; }

        public City CityId { get; set; }

        public virtual Role Role { get; set; }

        public bool IsInRole(int id)
        {
            return Role != null && Role.Id == id;
        }

        public Nullable<DateTime> BirthDate { get; set; }

        public Nullable<bool> IsActive { get; set; }

        public long Phone { get; set; }

        public string SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }

        public bool Male { get; set; }

        public bool Female { get; set; }

        public string Occupation { get; set; }
    }
}