using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoMVC.Models
{
    public class UserModel
    {
        [MinLength(5, ErrorMessage ="Ten it nhat 5 ky tu!")]
        public string Name { get; set; }
        [MinLength(6, ErrorMessage ="Do dai mat khau it nhat 6 ky tu!")]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}