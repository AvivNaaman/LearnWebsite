using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebsite.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(24, MinimumLength = 6)]
        [Remote(action: "VerifyUserName", controller: "account")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "VerifyEmail", controller: "account")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password and Vertification do not match.")]
        [DataType(DataType.Password)]
        public string PasswordVertification { get; set; }
    }
}
