using FYProject1Classes.LocationMgmt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FYProject1Classes.UserMgmt
{
    public class User
    {
        public User()
        {
            UserImage = new List<UserImage>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter This Field")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter This Field")]
        public string LoginID { get; set; }

        [Required(ErrorMessage = "Enter This Field")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter This Field")]
        public string Email { get; set; }

        public ICollection<UserImage> UserImage { get; set; }

        [Required(ErrorMessage = "Enter This Field")]
        public string FullAddress { get; set; }

        [Required(ErrorMessage = "Enter This Field")]
        public City CityId { get; set; }

        public virtual Role Role { get; set; }

        public bool IsInRole(int id)
        {
            return Role != null && Role.Id == id;
        }

        public Nullable<DateTime> BirthDate { get; set; }

        public Nullable<bool> IsActive { get; set; }

        [Required(ErrorMessage = "Enter This Field")]
        public long Phone { get; set; }


        public string SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }

        public bool Male { get; set; }

        public bool Female { get; set; }

        public string Occupation { get; set; }
    }
}