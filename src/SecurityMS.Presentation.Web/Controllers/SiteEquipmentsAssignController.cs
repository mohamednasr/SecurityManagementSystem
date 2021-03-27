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
    public class SiteEquipmentsAssignController : Controller
    {
        private readonly AppDbContext _context;

        public SiteEquipmentsAssignController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SiteEquipmentsAssign
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SiteEquipmentsAssignEntities.Include(s => s.EquipmentDetails).Include(s => s.SiteEquipment);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SiteEquipmentsAssign/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEquipmentsAssignEntity = await _context.SiteEquipmentsAssignEntities
                .Include(s => s.EquipmentDetails)
                .Include(s => s.SiteEquipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteEquipmentsAssignEntity == null)
            {
                return NotFound();
            }

            return View(siteEquipmentsAssignEntity);
        }

        // GET: SiteEquipmentsAssign/Create
        public IActionResult Create()
        {
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentDetailsEntities, "Id", "Name");
            ViewData["SiteEquipmenteId"] = new SelectList(_context.SiteEquipmentsEntities, "Id", "Name");
            return View();
        }

        // POST: SiteEquipmentsAssign/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiteEquipmenteId,EquipmentId,IsActive,Id")] SiteEquipmentsAssignEntity siteEquipmentsAssignEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siteEquipmentsAssignEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentDetailsEntities, "Id", "Name", siteEquipmentsAssignEntity.EquipmentId);
            ViewData["SiteEquipmenteId"] = new SelectList(_context.SiteEquipmentsEntities, "Id", "Name", siteEquipmentsAssignEntity.SiteEquipmenteId);
            return View(siteEquipmentsAssignEntity);
        }

        // GET: SiteEquipmentsAssign/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEquipmentsAssignEntity = await _context.SiteEquipmentsAssignEntities.FindAsync(id);
            if (siteEquipmentsAssignEntity == null)
            {
                return NotFound();
            }
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentDetailsEntities, "Id", "Name", siteEquipmentsAssignEntity.EquipmentId);
            ViewData["SiteEquipmenteId"] = new SelectList(_context.SiteEquipmentsEntities, "Id", "Name", siteEquipmentsAssignEntity.SiteEquipmenteId);
            return View(siteEquipmentsAssignEntity);
        }

        // POST: SiteEquipmentsAssign/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SiteEquipmenteId,EquipmentId,IsActive,Id")] SiteEquipmentsAssignEntity siteEquipmentsAssignEntity)
        {
            if (id != siteEquipmentsAssignEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteEquipmentsAssignEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteEquipmentsAssignEntityExists(siteEquipmentsAssignEntity.Id))
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
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentDetailsEntities, "Id", "Name", siteEquipmentsAssignEntity.EquipmentId);
            ViewData["SiteEquipmenteId"] = new SelectList(_context.SiteEquipmentsEntities, "Id", "Name", siteEquipmentsAssignEntity.SiteEquipmenteId);
            return View(siteEquipmentsAssignEntity);
        }

        // GET: SiteEquipmentsAssign/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEquipmentsAssignEntity = await _context.SiteEquipmentsAssignEntities
                .Include(s => s.EquipmentDetails)
                .Include(s => s.SiteEquipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteEquipmentsAssignEntity == null)
            {
                return NotFound();
            }

            return View(siteEquipmentsAssignEntity);
        }

        // POST: SiteEquipmentsAssign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var siteEquipmentsAssignEntity = await _context.SiteEquipmentsAssignEntities.FindAsync(id);
            _context.SiteEquipmentsAssignEntities.Remove(siteEquipmentsAssignEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteEquipmentsAssignEntityExists(long id)
        {
            return _context.SiteEquipmentsAssignEntities.Any(e => e.Id == id);
        }
    }
}
