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
    public class CountriesLookupController : Controller
    {
        private readonly AppDbContext _context;

        public CountriesLookupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CountriesLookup
        public async Task<IActionResult> Index()
        {
            return View(await _context.CountriesLookups.ToListAsync());
        }

        // GET: CountriesLookup/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countriesLookup = await _context.CountriesLookups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countriesLookup == null)
            {
                return NotFound();
            }

            return View(countriesLookup);
        }

        // GET: CountriesLookup/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CountriesLookup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] CountriesLookup countriesLookup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(countriesLookup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(countriesLookup);
        }

        // GET: CountriesLookup/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countriesLookup = await _context.CountriesLookups.FindAsync(id);
            if (countriesLookup == null)
            {
                return NotFound();
            }
            return View(countriesLookup);
        }

        // POST: CountriesLookup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Id")] CountriesLookup countriesLookup)
        {
            if (id != countriesLookup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countriesLookup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountriesLookupExists(countriesLookup.Id))
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
            return View(countriesLookup);
        }

        // GET: CountriesLookup/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countriesLookup = await _context.CountriesLookups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (countriesLookup == null)
            {
                return NotFound();
            }

            return View(countriesLookup);
        }

        // POST: CountriesLookup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var countriesLookup = await _context.CountriesLookups.FindAsync(id);
            _context.CountriesLookups.Remove(countriesLookup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountriesLookupExists(long id)
        {
            return _context.CountriesLookups.Any(e => e.Id == id);
        }
    }
}
