using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class SiteEquipmentsController : Controller
    {
        private readonly AppDbContext _context;

        public SiteEquipmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SiteEquipments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SiteEquipmentsEntities.Include(s => s.Equipment).Include(s => s.Site);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SiteEquipments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEquipmentsEntity = await _context.SiteEquipmentsEntities
                .Include(s => s.Equipment)
                .Include(s => s.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteEquipmentsEntity == null)
            {
                return NotFound();
            }

            return View(siteEquipmentsEntity);
        }

        // GET: SiteEquipments/Create
        public IActionResult Create()
        {
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentsEntities, "Id", "Name");
            ViewData["SiteId"] = new SelectList(_context.SitesEntities, "Id", "Name");
            return View();
        }

        public IActionResult Add(long? id)
        {
            SiteEquipmentsEntity siteEquipments = new SiteEquipmentsEntity();
            siteEquipments.SiteId = id.Value;
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentsEntities, "Id", "Name");
            ViewData["SiteId"] = new SelectList(_context.SitesEntities, "Id", "Name");
            return View("Create", siteEquipments);
        }

        // POST: SiteEquipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiteId,EquipmentId,EquipmentValue,EquipmentCount,Id")] SiteEquipmentsEntity siteEquipmentsEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siteEquipmentsEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentsEntities, "Id", "Name", siteEquipmentsEntity.EquipmentId);
            ViewData["SiteId"] = new SelectList(_context.SitesEntities, "Id", "Name", siteEquipmentsEntity.SiteId);
            return View(siteEquipmentsEntity);
        }

        // GET: SiteEquipments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEquipmentsEntity = await _context.SiteEquipmentsEntities.FindAsync(id);
            if (siteEquipmentsEntity == null)
            {
                return NotFound();
            }
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentsEntities, "Id", "Name", siteEquipmentsEntity.EquipmentId);
            ViewData["SiteId"] = new SelectList(_context.SitesEntities, "Id", "Name", siteEquipmentsEntity.SiteId);
            return View(siteEquipmentsEntity);
        }

        // POST: SiteEquipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SiteId,EquipmentId,EquipmentValue,EquipmentCount,Id")] SiteEquipmentsEntity siteEquipmentsEntity)
        {
            if (id != siteEquipmentsEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteEquipmentsEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteEquipmentsEntityExists(siteEquipmentsEntity.Id))
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
            ViewData["EquipmentId"] = new SelectList(_context.EquipmentsEntities, "Id", "Name", siteEquipmentsEntity.EquipmentId);
            ViewData["SiteId"] = new SelectList(_context.SitesEntities, "Id", "Name", siteEquipmentsEntity.SiteId);
            return View(siteEquipmentsEntity);
        }

        // GET: SiteEquipments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEquipmentsEntity = await _context.SiteEquipmentsEntities
                .Include(s => s.Equipment)
                .Include(s => s.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteEquipmentsEntity == null)
            {
                return NotFound();
            }

            return View(siteEquipmentsEntity);
        }

        // POST: SiteEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var siteEquipmentsEntity = await _context.SiteEquipmentsEntities.FindAsync(id);
            _context.SiteEquipmentsEntities.Remove(siteEquipmentsEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteEquipmentsEntityExists(long id)
        {
            return _context.SiteEquipmentsEntities.Any(e => e.Id == id);
        }
    }
}
