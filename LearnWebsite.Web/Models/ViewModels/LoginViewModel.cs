using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebsite.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(70, MinimumLength = 4)]
        public string UserNameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
