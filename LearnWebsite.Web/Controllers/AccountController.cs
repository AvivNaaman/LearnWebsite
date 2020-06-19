using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnWebsite.Web.Models.Entities;
using LearnWebsite.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnWebsite.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register() => View();

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([Bind("Email,Password,PasswordVertification,UserName")] RegisterViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var newUser = new AppUser(model.UserName, model.Email);
            var result = await _userManager.CreateAsync(newUser, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, true);

                if (!String.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", new { NewUser = true });
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login() => View();

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([Bind("UserNameOrEmail,Password")] LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            AppUser u;
            // if email, try finding by it. otherwise, try finding by username:
            try
            {
                new System.Net.Mail.MailAddress(model.UserNameOrEmail); // throws if not email
                u = await _userManager.FindByEmailAsync(model.UserNameOrEmail);
            }
            catch
            {
                u = await _userManager.FindByNameAsync(model.UserNameOrEmail);
            }

            if (u == null)
            {
                ModelState.AddModelError(String.Empty, "Invalid login attempt");
                return View(model);
            }

            var res = await _signInManager.PasswordSignInAsync(u, model.Password, true, false);

            if (!res.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Invalid login attempt");
                return View(model);
            }

            if (!String.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [AllowAnonymous]
        [AcceptVerbs("GET","POST")]
        public async Task<IActionResult> VerifyEmail(string email)
        {
            // returns whether there is no user with this email in the database
            return Json(!(await _userManager.Users.AnyAsync(u => u.NormalizedEmail == _userManager.NormalizeEmail(email))));
        }
        [AllowAnonymous]
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyUserName(string username)
        {
            // returns whether there is no user with this username in the database
            return Json(!(await _userManager.Users.AnyAsync(u => u.NormalizedUserName == _userManager.NormalizeName(username))));
        }

        public async Task<IActionResult> Logout(string returnUrl)
        {
            await _signInManager.SignOutAsync();
            if (!String.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
