using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class SupplyTypesController : Controller
    {

        private readonly AppDbContext _context;

        public SupplyTypesController(AppDbContext context)
        {
            _context = context;
        }
        // GET: SupplyTypeController
        public async Task<IActionResult> Index()
        {

            return View(await _context.SupplyTypes.Where(s => !s.IsDeleted).ToListAsync());
        }

        // GET: SupplyTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplyTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplyName,Id")] SupplyTypes supplyType)
        {
            if (ModelState.IsValid)
            {
                supplyType.create(HttpContext.User.Identity.Name);
                _context.Add(supplyType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplyType);
        }

        // GET: SupplyTypeController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.SupplyTypes == null)
            {
                return NotFound();
            }

            var supplyType = await _context.SupplyTypes.FindAsync(id);
            if (supplyType == null)
            {
                return NotFound();
            }
            return View(supplyType);
        }

        // POST: SupplyTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplyName,Id,CreatedAt,CreatedBy,IsDeleted")] SupplyTypes supplyType)
        {
            if (id != supplyType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    supplyType.update(HttpContext.User.Identity.Name);
                    _context.Update(supplyType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplyTypeExists(supplyType.Id))
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
            return View(supplyType);
        }

        // GET: SupplyTypeController/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.SupplyTypes == null)
            {
                return NotFound();
            }

            var supplier = await _context.SupplyTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: SupplyTypeController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Suppliers == null)
            {
                return Problem("Entity set 'WebApplication1Context.Supplier'  is null.");
            }
            var supplyType = await _context.SupplyTypes.FindAsync(id);
            if (supplyType != null)
            {
                supplyType.Delete(HttpContext.User.Identity.Name);
                _context.SupplyTypes.Update(supplyType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplyTypeExists(long id)
        {
            return _context.SupplyTypes.Any(e => e.Id == id);
        }
    }
}
