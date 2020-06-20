using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnWebsite.Web.Data;
using LearnWebsite.Web.Models.Entities;

namespace LearnWebsite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = AppConstants.Roles.Admin)]
    public class PagesController : Controller
    {
        private readonly AppDbContext _context;

        public PagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Pages
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Pages.Include(c => c.Unit);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Pages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursePage = await _context.Pages
                .Include(c => c.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coursePage == null)
            {
                return NotFound();
            }

            return View(coursePage);
        }

        // GET: Pages/Create
        public IActionResult Create()
        {
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "DisplayName");
            return View();
        }

        // POST: Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,HtmlContent,UnitId")] CoursePage coursePage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coursePage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UnitId"] = new SelectList(_context.Courses, "Id", "DisplayName", coursePage.Unit.CourseId);
            return View(coursePage);
        }

        // GET: Pages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursePage = await _context.Pages.FindAsync(id);
            if (coursePage == null)
            {
                return NotFound();
            }
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Id", coursePage.UnitId);
            return View(coursePage);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,HtmlContent,UnitId")] CoursePage coursePage)
        {
            if (id != coursePage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coursePage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursePageExists(coursePage.Id))
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
            ViewData["UnitId"] = new SelectList(_context.Units, "Id", "Id", coursePage.UnitId);
            return View(coursePage);
        }

        // GET: Pages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursePage = await _context.Pages
                .Include(c => c.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coursePage == null)
            {
                return NotFound();
            }

            return View(coursePage);
        }

        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coursePage = await _context.Pages.FindAsync(id);
            _context.Pages.Remove(coursePage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursePageExists(int id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }
    }
}
