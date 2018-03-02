using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYProject1.Models
{
    public class recoveryEmail
    {
        [Required, Display(Name = "Email"), EmailAddress]
        public string Email { get; set; }
    }
}