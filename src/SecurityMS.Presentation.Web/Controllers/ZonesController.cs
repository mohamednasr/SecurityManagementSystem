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
    public class ZonesController : Controller
    {
        private readonly AppDbContext _context;

        public ZonesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Zones
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ZonesEntities.Include(z => z.Government).Select(z => new ZoneModel()
            {
                Id = z.Id,
                Name = z.Name,
                Government = new GovernmentModel()
                {
                    Id = z.GovernmentId,
                    Name = z.Government.Name
                }
            });
            var Governments = new List<GovernmentEntity>();
            Governments.Add(new GovernmentEntity() { Id = 0, Name = "أختر المحافظة" });
            Governments.AddRange(await _context.GovernmentEntities.ToListAsync());
            ViewData["GovernmentId"] = new SelectList(Governments, "Id", "Name");

            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> search(ZoneModel zoneSearch)
        {
            var appDbContext = _context.ZonesEntities.Include(z => z.Government).Where(z => z.GovernmentId == zoneSearch.GovernmentId || z.Name.Contains(zoneSearch.Name)).Select(z => new ZoneModel()
            {
                Id = z.Id,
                Name = z.Name,
                Government = new GovernmentModel()
                {
                    Id = z.GovernmentId,
                    Name = z.Government.Name
                }
            });
            ViewData["GovernmentId"] = new SelectList(_context.GovernmentEntities, "Id", "Name");

            return View("Index", await appDbContext.ToListAsync());
        }

        // GET: Zones/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoneEntity = await _context.ZonesEntities
                .Include(z => z.Government)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zoneEntity == null)
            {
                return NotFound();
            }
            ZoneModel zone = new ZoneModel()
            {
                Name = zoneEntity.Name,
                GovernmentId = zoneEntity.GovernmentId,
                Government = new GovernmentModel()
                {
                    Id = zoneEntity.GovernmentId,
                    Name = zoneEntity.Government.Name
                }
            };
            return View(zone);
        }

        // GET: Zones/Create
        public IActionResult Create()
        {
            ViewData["GovernmentId"] = new SelectList(_context.GovernmentEntities, "Id", "Name");
            return View();
        }

        // POST: Zones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,GovernmentId,Id")] ZoneModel zone)
        {
            if (ModelState.IsValid)
            {
                ZonesEntity zoneEntity = new ZonesEntity()
                {
                    Name = zone.Name,
                    GovernmentId = zone.GovernmentId
                };
                _context.Add(zoneEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GovernmentId"] = new SelectList(_context.GovernmentEntities, "Id", "Name", zone.GovernmentId);
            return View(zone);
        }

        // GET: Zones/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoneEntity = await _context.ZonesEntities.FindAsync(id);
            if (zoneEntity == null)
            {
                return NotFound();
            }
            ZoneModel zone = new ZoneModel()
            {
                Id = zoneEntity.Id,
                Name = zoneEntity.Name,
                GovernmentId = zoneEntity.GovernmentId
            };
            ViewData["GovernmentId"] = new SelectList(_context.GovernmentEntities, "Id", "Name", zone.GovernmentId);
            return View(zone);
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,GovernmentId,Id")] ZoneModel zone)
        {
            if (id != zone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ZonesEntity zoneEntity = new ZonesEntity()
                    {
                        Id = zone.Id,
                        Name = zone.Name,
                        GovernmentId = zone.GovernmentId
                    };
                    _context.Update(zoneEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZonesEntityExists(zone.Id))
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
            ViewData["GovernmentId"] = new SelectList(_context.GovernmentEntities, "Id", "Name", zone.GovernmentId);
            return View(zone);
        }

        // GET: Zones/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zonesEntity = await _context.ZonesEntities
                .Include(z => z.Government)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zonesEntity == null)
            {
                return NotFound();
            }

            return View(zonesEntity);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var zonesEntity = await _context.ZonesEntities.FindAsync(id);
            _context.ZonesEntities.Remove(zonesEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]

        public async Task<List<ZonesEntity>> getZone(long id)
        {
            if (id == 0)
            {
                return await _context.ZonesEntities.ToListAsync();
            }
            var zonesEntity = await _context.ZonesEntities.Where(z => z.Government.Id == id).ToListAsync();
            return zonesEntity;
        }

        private bool ZonesEntityExists(long id)
        {
            return _context.ZonesEntities.Any(e => e.Id == id);
        }
    }
}
