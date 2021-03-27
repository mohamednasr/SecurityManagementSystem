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
    public class GovernmentController : Controller
    {
        private readonly AppDbContext _context;

        public GovernmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Government
        public async Task<IActionResult> Index()
        {
            return View(await _context.GovernmentEntities.ToListAsync());
        }

        // GET: Government/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var governmentEntity = await _context.GovernmentEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (governmentEntity == null)
            {
                return NotFound();
            }

            return View(governmentEntity);
        }

        // GET: Government/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Government/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] GovernmentEntity governmentEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(governmentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(governmentEntity);
        }

        // GET: Government/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var governmentEntity = await _context.GovernmentEntities.FindAsync(id);
            if (governmentEntity == null)
            {
                return NotFound();
            }
            return View(governmentEntity);
        }

        // POST: Government/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Id")] GovernmentEntity governmentEntity)
        {
            if (id != governmentEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(governmentEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GovernmentEntityExists(governmentEntity.Id))
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
            return View(governmentEntity);
        }

        // GET: Government/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var governmentEntity = await _context.GovernmentEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (governmentEntity == null)
            {
                return NotFound();
            }

            return View(governmentEntity);
        }

        // POST: Government/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var governmentEntity = await _context.GovernmentEntities.FindAsync(id);
            _context.GovernmentEntities.Remove(governmentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GovernmentEntityExists(long id)
        {
            return _context.GovernmentEntities.Any(e => e.Id == id);
        }
    }
}
