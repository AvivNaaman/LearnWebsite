using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnWebsite.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace LearnWebsite.Web.Controllers
{
    public class LearnController : Controller
    {
        private readonly AppDbContext _context;

        public LearnController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string course, string page)
        {
            // redirect to home page if course is not specified
            if (String.IsNullOrEmpty(course)) return RedirectToAction("Index", "Home");
            // return first course page
            if (String.IsNullOrEmpty(page)) return await GetFirstCoursePage(course);
            else return await GetCoursePage(course, page);
        }

        private async Task<IActionResult> GetFirstCoursePage(string courseUrlName)
        {
            throw new NotImplementedException();
            /*var page = */
        }

        private async Task<IActionResult> GetCoursePage(string courseUrlName, string pageUrlName)
        {
            throw new NotImplementedException();
        }
    }
}
