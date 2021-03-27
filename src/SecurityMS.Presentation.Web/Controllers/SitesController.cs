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

            return View(sitesEntity);
        }

        // GET: Sites/Create
        public IActionResult Create()
        {
            ViewData["ZoneId"] = new SelectList(_context.ZonesEntities, "Id", "Name");
            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,ZoneId,Id")] SitesEntity sitesEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sitesEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ZoneId"] = new SelectList(_context.ZonesEntities, "Id", "Name", sitesEntity.ZoneId);
            return View(sitesEntity);
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
        public async Task<IActionResult> Edit(long id, [Bind("Name,Address,ZoneId,Id")] SitesEntity sitesEntity)
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

        private bool SitesEntityExists(long id)
        {
            return _context.SitesEntities.Any(e => e.Id == id);
        }
    }
}
