using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyArtCollection.Web.Data;

namespace MyArtCollection.Web.Controllers
{
    public class TechniquesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TechniquesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Techniques
        public async Task<IActionResult> Index()
        {
            return View(await _context.Techniques.ToListAsync());
        }

        // GET: Techniques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technique = await _context.Techniques
                .FirstOrDefaultAsync(m => m.Id == id);
            if (technique == null)
            {
                return NotFound();
            }

            return View(technique);
        }

        // GET: Techniques/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Techniques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Technique technique)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technique);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technique);
        }

        // GET: Techniques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technique = await _context.Techniques.FindAsync(id);
            if (technique == null)
            {
                return NotFound();
            }
            return View(technique);
        }

        // POST: Techniques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Technique technique)
        {
            if (id != technique.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technique);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechniqueExists(technique.Id))
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
            return View(technique);
        }

        // GET: Techniques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technique = await _context.Techniques
                .FirstOrDefaultAsync(m => m.Id == id);
            if (technique == null)
            {
                return NotFound();
            }

            return View(technique);
        }

        // POST: Techniques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technique = await _context.Techniques.FindAsync(id);
            if (technique != null)
            {
                _context.Techniques.Remove(technique);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechniqueExists(int id)
        {
            return _context.Techniques.Any(e => e.Id == id);
        }
    }
}
