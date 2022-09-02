using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class EquipmentsController : Controller
    {
        private readonly AppDbContext _context;

        public EquipmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Equipments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EquipmentsEntities.Include(e => e.EquipmentType).Include(e => e.Manufacturing);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentsEntity = await _context.EquipmentsEntities
                .Include(e => e.EquipmentType)
                .Include(e => e.Manufacturing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentsEntity == null)
            {
                return NotFound();
            }

            return View(equipmentsEntity);
        }

        // GET: Equipments/Create
        public async Task<IActionResult> Create()
        {
            var EquipmentType = new List<EquipmentTypesLookup>();
            EquipmentType.Add(new EquipmentTypesLookup() { Id = 0, Name = "أختر نوع الأداه" });
            EquipmentType.AddRange(await _context.EquipmentTypesLookups.ToListAsync());
            ViewData["EquipmentTypeId"] = new SelectList(EquipmentType, "Id", "Name");

            var Manufactures = new List<CountriesLookup>();
            Manufactures.Add(new CountriesLookup() { Id = 0, Name = "أختر دولة المنشأ" });
            Manufactures.AddRange(await _context.CountriesLookups.ToListAsync());
            ViewData["ManufactureId"] = new SelectList(Manufactures, "Id", "Name");
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ManufactureId,EquipmentTypeId,EquipmentTotalCount,Id")] EquipmentsEntity equipmentsEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipmentsEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var EquipmentType = new List<EquipmentTypesLookup>();
            EquipmentType.Add(new EquipmentTypesLookup() { Id = 0, Name = "أختر نوع الأداه" });
            EquipmentType.AddRange(await _context.EquipmentTypesLookups.ToListAsync());
            ViewData["EquipmentTypeId"] = new SelectList(EquipmentType, "Id", "Name", equipmentsEntity.EquipmentTypeId);

            var Manufactures = new List<CountriesLookup>();
            Manufactures.Add(new CountriesLookup() { Id = 0, Name = "أختر دولة المنشأ" });
            Manufactures.AddRange(await _context.CountriesLookups.ToListAsync());
            ViewData["ManufactureId"] = new SelectList(Manufactures, "Id", "Name", equipmentsEntity.ManufactureId);

            return View(equipmentsEntity);
        }

        // GET: Equipments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentsEntity = await _context.EquipmentsEntities.FindAsync(id);
            if (equipmentsEntity == null)
            {
                return NotFound();
            }
            var EquipmentType = new List<EquipmentTypesLookup>();
            EquipmentType.Add(new EquipmentTypesLookup() { Id = 0, Name = "أختر نوع الأداه" });
            EquipmentType.AddRange(await _context.EquipmentTypesLookups.ToListAsync());
            ViewData["EquipmentTypeId"] = new SelectList(EquipmentType, "Id", "Name", equipmentsEntity.EquipmentTypeId);

            var Manufactures = new List<CountriesLookup>();
            Manufactures.Add(new CountriesLookup() { Id = 0, Name = "أختر دولة المنشأ" });
            Manufactures.AddRange(await _context.CountriesLookups.ToListAsync());
            ViewData["ManufactureId"] = new SelectList(Manufactures, "Id", "Name", equipmentsEntity.ManufactureId);

            return View(equipmentsEntity);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Description,ManufactureId,EquipmentTypeId,EquipmentTotalCount,Id")] EquipmentsEntity equipmentsEntity)
        {
            if (id != equipmentsEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipmentsEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentsEntityExists(equipmentsEntity.Id))
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
            var EquipmentType = new List<EquipmentTypesLookup>();
            EquipmentType.Add(new EquipmentTypesLookup() { Id = 0, Name = "أختر نوع الأداه" });
            EquipmentType.AddRange(await _context.EquipmentTypesLookups.ToListAsync());
            ViewData["EquipmentTypeId"] = new SelectList(EquipmentType, "Id", "Name", equipmentsEntity.EquipmentTypeId);

            var Manufactures = new List<CountriesLookup>();
            Manufactures.Add(new CountriesLookup() { Id = 0, Name = "أختر دولة المنشأ" });
            Manufactures.AddRange(await _context.CountriesLookups.ToListAsync());
            ViewData["ManufactureId"] = new SelectList(Manufactures, "Id", "Name", equipmentsEntity.ManufactureId);

            return View(equipmentsEntity);
        }

        // GET: Equipments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipmentsEntity = await _context.EquipmentsEntities
                .Include(e => e.EquipmentType)
                .Include(e => e.Manufacturing)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipmentsEntity == null)
            {
                return NotFound();
            }

            return View(equipmentsEntity);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var equipmentsEntity = await _context.EquipmentsEntities.FindAsync(id);
            _context.EquipmentsEntities.Remove(equipmentsEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentsEntityExists(long id)
        {
            return _context.EquipmentsEntities.Any(e => e.Id == id);
        }
    }
}
