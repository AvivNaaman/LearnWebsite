using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LearnWebsite.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LearnWebsite.Web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorController : Controller
    {
        /// <summary>
        /// a List of supported status code pages
        /// </summary>
        public static readonly List<int> AvailableStatusCodeCustomViews = new List<int>() { 401, 403, 404, };

        [Route("Error/{code?}")]
        public IActionResult ErrorHandler(int? code)
        {
            if (!code.HasValue)
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            if (AvailableStatusCodeCustomViews.Any(s => s == code.Value)) return View("_" + code.ToString());
            return View("Index");
        }
    }
}
