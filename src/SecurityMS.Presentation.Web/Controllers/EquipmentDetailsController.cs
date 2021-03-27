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
    public class EquipmentDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public EquipmentDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EquipmentDetails
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EquipmentDetailsEntities.Include(e => e.Equipment);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EquipmentDetails/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentDetailsEntity = await _context.EquipmentDetailsEntities
                .Include(e => e.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentDetailsEntity == null)
            {
                return NotFound();
            }

            return View(equipmentDetailsEntity);
        }

        // GET: EquipmentDetails/Create
        public IActionResult Create()
        {
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentsEntities, "Id", "Name");
            return View();
        }

        // POST: EquipmentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipmentId,Serial,ProductionDate,EquipmentPrice,Id")] EquipmentDetailsEntity equipmentDetailsEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipmentDetailsEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentsEntities, "Id", "Name", equipmentDetailsEntity.EquipmentId);
            return View(equipmentDetailsEntity);
        }

        // GET: EquipmentDetails/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentDetailsEntity = await _context.EquipmentDetailsEntities.FindAsync(id);
            if (equipmentDetailsEntity == null)
            {
                return NotFound();
            }
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentsEntities, "Id", "Name", equipmentDetailsEntity.EquipmentId);
            return View(equipmentDetailsEntity);
        }

        // POST: EquipmentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("EquipmentId,Serial,ProductionDate,EquipmentPrice,Id")] EquipmentDetailsEntity equipmentDetailsEntity)
        {
            if (id != equipmentDetailsEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentDetailsEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentDetailsEntityExists(equipmentDetailsEntity.Id))
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
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentsEntities, "Id", "Name", equipmentDetailsEntity.EquipmentId);
            return View(equipmentDetailsEntity);
        }

        // GET: EquipmentDetails/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentDetailsEntity = await _context.EquipmentDetailsEntities
                .Include(e => e.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentDetailsEntity == null)
            {
                return NotFound();
            }

            return View(equipmentDetailsEntity);
        }

        // POST: EquipmentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var equipmentDetailsEntity = await _context.EquipmentDetailsEntities.FindAsync(id);
            _context.EquipmentDetailsEntities.Remove(equipmentDetailsEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentDetailsEntityExists(long id)
        {
            return _context.EquipmentDetailsEntities.Any(e => e.Id == id);
        }
    }
}
