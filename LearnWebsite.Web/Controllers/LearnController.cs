using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnWebsite.Web.Data;
using LearnWebsite.Web.Extensions;
using LearnWebsite.Web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnWebsite.Web.Controllers
{
    public class LearnController : Controller
    {
        private readonly AppDbContext _context;

        public LearnController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Learn/{course}/{unit?}/{page?}", Name = "LearnRoute")]
        public async Task<IActionResult> Learn(string course, string unit, string page)
        {
            // redirect to home page if course is not specified
            if (String.IsNullOrWhiteSpace(course)) return RedirectToAction("Index", "Home");
            var p = await GetPage(course, unit, page);
            if (p == null) return NotFound();
            return View("Page", p);
        }
        [NonAction]
        private async Task<CoursePage> GetPage(string course, string unit, string page)
        {
            if (String.IsNullOrWhiteSpace(course)) return null;
            Course c = await _context.Courses.Include(c => c.Units)
                .ThenInclude(u => u.Pages)
                .FirstOrDefaultAsync(c => c.UrlName == course);

            if (c == null) return null;
            c = c.SortUnitsAndPages();

            // if no unit url name specified, get first page of first unit
            if (String.IsNullOrWhiteSpace(unit)) return c.Units.ElementAt(0).Pages.ElementAt(0);
            CourseUnit u = c.Units.FirstOrDefault(u => u.UrlName == unit);
            if (u == null) return null;
            // if no page url name, return first page 
            if (String.IsNullOrWhiteSpace(page)) return u.Pages.ElementAt(0);

            return u.Pages.FirstOrDefault(p => p.UrlName == page);
        }
        [Route("Course/{course}", Name = "CourseInfo")]
        [HttpGet]
        public async Task<IActionResult> CourseInfo(string course) // id = course url name
        {
            // display course details if unit is not specified
            var c = await _context.Courses.Include(c => c.Units).FirstOrDefaultAsync(c => c.UrlName == course);
            if (c == null) return NotFound();
            return View(c);
        }
    }
}
