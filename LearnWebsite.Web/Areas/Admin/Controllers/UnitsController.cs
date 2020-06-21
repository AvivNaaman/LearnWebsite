using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnWebsite.Web.Data;
using LearnWebsite.Web.Models.Entities;
using LearnWebsite.Web.Extensions;

namespace LearnWebsite.Web.Areas.Admin.controllers
{
    [Area("Admin")]
    public class UnitsController : Controller
    {
        private readonly AppDbContext _context;

        public UnitsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Units
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Units.Include(c => c.Course);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Units/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseUnit = await _context.Units
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseUnit == null)
            {
                return NotFound();
            }

            return View(courseUnit);
        }

        // GET: Admin/Units/Create
        public IActionResult Create(int? courseId)
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "DisplayName", courseId);
            return View();
        }

        // POST: Admin/Units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,DisplayName,UrlName")] CourseUnit courseUnit)
        {
            if (ModelState.IsValid)
            {
                // url address
                if (String.IsNullOrEmpty(courseUnit.UrlName))
                {
                    // leave all
                    courseUnit.UrlName = courseUnit.DisplayName.ConvertToUrlName();
                    if (courseUnit.UrlName.Length < 4) // error if too small
                    {
                        ModelState.AddModelError("UrlName", "Choose a url name which contains only numbers, digits, hyphen and underbar, or choose a course name with more english chars.");
                        return View(courseUnit);
                    }
                }
                // put the unit as last in the course
                courseUnit.InCourseOrder = await _context.Units.Where(u => u.CourseId == courseUnit.CourseId).MaxAsync(u => u.InCourseOrder) + 1;
                _context.Add(courseUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "DisplayName", courseUnit.CourseId);
            return View(courseUnit);
        }

        // GET: Admin/Units/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseUnit = await _context.Units.FindAsync(id);
            if (courseUnit == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Description", courseUnit.CourseId);
            return View(courseUnit);
        }

        // POST: Admin/Units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,DisplayName,InCourseOrder,UrlName")] CourseUnit courseUnit)
        {
            if (id != courseUnit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // url address
                    if (String.IsNullOrEmpty(courseUnit.UrlName))
                    {
                        // leave all
                        courseUnit.UrlName = courseUnit.DisplayName.ConvertToUrlName();
                        if (courseUnit.UrlName.Length < 4) // error if too small
                        {
                            ModelState.AddModelError("UrlName", "Choose a url name which contains only numbers, digits, hyphen and underbar, or choose a course name with more english chars.");
                            return View(courseUnit);
                        }
                    }
                    _context.Update(courseUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseUnitExists(courseUnit.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Description", courseUnit.CourseId);
            return View(courseUnit);
        }

        // GET: Admin/Units/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseUnit = await _context.Units
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseUnit == null)
            {
                return NotFound();
            }

            return View(courseUnit);
        }

        // POST: Admin/Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseUnit = await _context.Units.FindAsync(id);
            _context.Units.Remove(courseUnit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseUnitExists(int id)
        {
            return _context.Units.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GetUnitsAsJson(int courseId)
        {
            return Json(await _context.Units.Where(u => u.CourseId == courseId).ToListAsync());
        }
    }
}
