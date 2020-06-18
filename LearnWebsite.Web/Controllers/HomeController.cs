using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LearnWebsite.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using LearnWebsite.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace LearnWebsite.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Maximum amount of "latest courses" to display on home page carousel
        /// </summary>
        private const int MaxLatestCoursesCount = 5;
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses
                .OrderByDescending(c => c.Created)
                .Take(MaxLatestCoursesCount).ToListAsync();
            return View(courses);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
