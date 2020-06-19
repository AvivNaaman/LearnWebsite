using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnWebsite.Web.Data;
using LearnWebsite.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using LearnWebsite.Web.Areas.Admin.Models.ViewModels;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Internal;
using SixLabors.ImageSharp;
using LearnWebsite.Web.Extnsions;
using LearnWebsite.Web.Extensions;

namespace LearnWebsite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = AppConstants.Roles.Admin)]
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // query course WITH units & pages
            var course = await GetFullSortedCourse(id.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DisplayName,Description,Viewed")] Course course, [FromForm] IFormFile ImageUpload)
        {
            if (ModelState.IsValid)
            {
                if (ImageUpload != null)
                {
                    course.ImageSrc = await ImageUpload?.ScaleAndConvertToBase64(); // image upload
                }
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await GetFullSortedCourse(id.Value);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        class OrderedUnit
        {
            public int id { get; set; }
            public List<int> Values { get; set; }
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] EditCourseViewModel course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // handle order changing
                    if (!String.IsNullOrEmpty(course.UnitsPagesOrder))
                    {
                        var orderedUnits = JsonConvert.DeserializeObject<List<OrderedUnit>>(course.UnitsPagesOrder);
                        var units = (await _context.Units.Include(c => c.Pages).Where(c => c.CourseId == id).ToListAsync());
                        // iterate over units order
                        for (int i = 0; i < orderedUnits.Count; i++)
                        {
                            // set order of unit: find it and assign the new
                            var orderedUnit = orderedUnits[i];
                            var u = units.FirstOrDefault(u => u.Id == orderedUnit.id);
                            if (u == null) throw new Exception("Someone tried to reorder unit that doesn't exist any more!");
                            u.InCourseOrder = i;
                            _context.Update(u); // update unit
                            // set order of pages in unit
                            for (int j = 0; j < orderedUnit.Values.Count; j++)
                            {
                                var p = u.Pages.FirstOrDefault(p => p.Id == orderedUnit.Values[j]);
                                if (p == null) throw new Exception("Someone tried to reorder page that doesn't exist.");
                                p.InUnitOrder = j; // set order
                                _context.Update(p); // update
                            }
                        }
                    }
                    // handle new image upload
                    if (course.ImageUpload != null)
                    {
                        course.ImageSrc = await course.ImageUpload?.ScaleAndConvertToBase64(); // image upload /* TODO: Implement in view */
                    }
                    // handle other details
                    _context.Update((Course)course); // cast to remove all the extra fields
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        /// <summary>
        /// Returns the course by it's id
        /// </summary>
        /// <param name="id">The id to look for course with</param>
        /// <returns>The course</returns>
        private async Task<Course> GetFullSortedCourse(int id)
        {
            return (await _context.Courses
                .Include(c => c.Units).ThenInclude(u => u.Pages)
                .FirstOrDefaultAsync(m => m.Id == id)).SortUnitsAndPages();
        }
    }
}
