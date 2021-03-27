using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class ShiftTypesController : Controller
    {
        private readonly AppDbContext _context;

        public ShiftTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ShiftTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShiftTypesLookups.ToListAsync());
        }

        // GET: ShiftTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftTypesLookup = await _context.ShiftTypesLookups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shiftTypesLookup == null)
            {
                return NotFound();
            }

            return View(shiftTypesLookup);
        }

        // GET: ShiftTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShiftTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] ShiftTypesLookup shiftTypesLookup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shiftTypesLookup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shiftTypesLookup);
        }

        // GET: ShiftTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftTypesLookup = await _context.ShiftTypesLookups.FindAsync(id);
            if (shiftTypesLookup == null)
            {
                return NotFound();
            }
            return View(shiftTypesLookup);
        }

        // POST: ShiftTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Id")] ShiftTypesLookup shiftTypesLookup)
        {
            if (id != shiftTypesLookup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shiftTypesLookup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftTypesLookupExists(shiftTypesLookup.Id))
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
            return View(shiftTypesLookup);
        }

        // GET: ShiftTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shiftTypesLookup = await _context.ShiftTypesLookups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shiftTypesLookup == null)
            {
                return NotFound();
            }

            return View(shiftTypesLookup);
        }

        // POST: ShiftTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var shiftTypesLookup = await _context.ShiftTypesLookups.FindAsync(id);
            _context.ShiftTypesLookups.Remove(shiftTypesLookup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftTypesLookupExists(long id)
        {
            return _context.ShiftTypesLookups.Any(e => e.Id == id);
        }
    }
}
