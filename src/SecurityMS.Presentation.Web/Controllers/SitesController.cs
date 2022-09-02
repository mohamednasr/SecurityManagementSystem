using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Core.Models;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class SitesController : Controller
    {
        private readonly AppDbContext _context;

        public SitesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Sites
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SitesEntities.Include(s => s.zone);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Sites/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sitesEntity = await _context.SitesEntities
                .Include(s => s.zone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sitesEntity == null)
            {
                return NotFound();
            }
            var employees = await _context.SiteEmployeesEntities.Include(s => s.ShiftType).Include(s => s.Job).Where(s => s.SiteId == id).ToListAsync();
            var equipments = await _context.SiteEquipmentsEntities.Where(s => s.SiteId == id).ToListAsync();

            SiteModel site = new SiteModel()
            {
                Id = sitesEntity.Id,
                Name = sitesEntity.Name,
                Address = sitesEntity.Address,
                ZoneId = sitesEntity.ZoneId,
                zone = sitesEntity.zone,
                Transportations = sitesEntity.Transportations,
                SiteEmployees = employees,
                SiteEquipments = equipments
            };

            if (sitesEntity == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // GET: Sites/Create
        public IActionResult Create()
        {
            ViewData["ZoneId"] = new SelectList(_context.ZonesEntities, "Id", "Name");
            return View();
        }

        public async Task<IActionResult> CreateSiteForContract(long? Id)
        {
            SiteModel site = new SiteModel();
            site.ContractId = Id.Value;
            ViewData["ZoneId"] = new SelectList(_context.ZonesEntities, "Id", "Name");
            return View("Create", site);
        }

        // POST: Sites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,ZoneId,Id,ContractId, Transportations")] SiteModel site)
        {
            if (ModelState.IsValid)
            {
                SitesEntity siteEntity = new SitesEntity()
                {
                    Name = site.Name,
                    Address = site.Address,
                    ZoneId = site.ZoneId
                };
                if (site.ContractId != 0)
                {
                    siteEntity.Contracts = await _context.ContractsEntities.Where(c => c.Id == site.ContractId).FirstOrDefaultAsync();
                }
                _context.Add(siteEntity);
                await _context.SaveChangesAsync();
                if (site.ContractId != null)
                {
                    return RedirectToAction(nameof(Details), "Contracts", new { id = site.ContractId });
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ZoneId"] = new SelectList(_context.ZonesEntities, "Id", "Name", site.ZoneId);
            return View(site);
        }

        // GET: Sites/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sitesEntity = await _context.SitesEntities.FindAsync(id);
            if (sitesEntity == null)
            {
                return NotFound();
            }
            ViewData["ZoneId"] = new SelectList(_context.ZonesEntities, "Id", "Name", sitesEntity.ZoneId);
            return View(sitesEntity);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Address,ZoneId,Id, Transportations")] SitesEntity sitesEntity)
        {
            if (id != sitesEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sitesEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SitesEntityExists(sitesEntity.Id))
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
            ViewData["ZoneId"] = new SelectList(_context.ZonesEntities, "Id", "Name", sitesEntity.ZoneId);
            return View(sitesEntity);
        }

        // GET: Sites/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sitesEntity = await _context.SitesEntities
                .Include(s => s.zone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sitesEntity == null)
            {
                return NotFound();
            }

            return View(sitesEntity);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var sitesEntity = await _context.SitesEntities.FindAsync(id);
            _context.SitesEntities.Remove(sitesEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Get: sites/{zoneId}
        public async Task<List<SitesEntity>> getSitesByZone(long id)
        {
            if (id == 0)
            {
                return await _context.SitesEntities.ToListAsync();
            }
            var sites = await _context.SitesEntities.Where(z => z.ZoneId == id).ToListAsync();
            return sites;
        }

        private bool SitesEntityExists(long id)
        {
            return _context.SitesEntities.Any(e => e.Id == id);
        }
    }
}
