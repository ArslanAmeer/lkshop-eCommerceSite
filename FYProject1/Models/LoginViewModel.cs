using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FYProject1.Models
{
    public class LoginViewModel
    {
        [Display(Name ="LoginId")]
        [Required(ErrorMessage = "The Username field is Required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", 
                                ErrorMessage = "Email Not Varified !!")]
        public string LoginId { get; set; }


        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        [Required]
        [StringLength(50,MinimumLength =8,ErrorMessage ="Password Is Too Short..!!")]
        public string Password { get; set; }


        [Display(Name ="RememberMe")]
        public bool RememberMe { get; set; }




    }
}