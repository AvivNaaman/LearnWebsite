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
        public async Task<IActionResult> Index(string course, string unit, string page)
        {
            // redirect to home page if course is not specified
            if (String.IsNullOrWhiteSpace(course)) return RedirectToAction("Index", "Home");
            var p = await GetPage(course, unit, page);
            if (p == null) return NotFound();
            return View(p);
        }

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
    }
}
