using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebsite.Web.Models.Entities
{
    /// <summary>
    /// A user in the application
    /// <!-- For future use -->
    /// </summary>
    public class AppUser : IdentityUser
    {
        public AppUser() : base()
        {

        }

        public AppUser(string username, string email) : base(username)
        {
            Email = email;
        }
    }
}
