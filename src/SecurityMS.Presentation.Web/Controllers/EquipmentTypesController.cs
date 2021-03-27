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
    public class EquipmentTypesController : Controller
    {
        private readonly AppDbContext _context;

        public EquipmentTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EquipmentTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.EquipmentTypesLookups.ToListAsync());
        }

        // GET: EquipmentTypes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentTypesLookup = await _context.EquipmentTypesLookups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentTypesLookup == null)
            {
                return NotFound();
            }

            return View(equipmentTypesLookup);
        }

        // GET: EquipmentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EquipmentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] EquipmentTypesLookup equipmentTypesLookup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipmentTypesLookup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipmentTypesLookup);
        }

        // GET: EquipmentTypes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentTypesLookup = await _context.EquipmentTypesLookups.FindAsync(id);
            if (equipmentTypesLookup == null)
            {
                return NotFound();
            }
            return View(equipmentTypesLookup);
        }

        // POST: EquipmentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Id")] EquipmentTypesLookup equipmentTypesLookup)
        {
            if (id != equipmentTypesLookup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentTypesLookup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentTypesLookupExists(equipmentTypesLookup.Id))
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
            return View(equipmentTypesLookup);
        }

        // GET: EquipmentTypes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentTypesLookup = await _context.EquipmentTypesLookups
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentTypesLookup == null)
            {
                return NotFound();
            }

            return View(equipmentTypesLookup);
        }

        // POST: EquipmentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var equipmentTypesLookup = await _context.EquipmentTypesLookups.FindAsync(id);
            _context.EquipmentTypesLookups.Remove(equipmentTypesLookup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentTypesLookupExists(long id)
        {
            return _context.EquipmentTypesLookups.Any(e => e.Id == id);
        }
    }
}
