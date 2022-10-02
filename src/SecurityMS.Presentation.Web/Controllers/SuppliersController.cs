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
    public class SuppliersController : Controller
    {
        private readonly AppDbContext _context;

        public SuppliersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Suppliers.Include(s => s.supplyType).Where(s => !s.IsDeleted).ToListAsync());
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.Include(s => s.supplyType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public async Task<IActionResult> Create()
        {
            var supplyTypes = new List<SupplyTypes>();
            supplyTypes.Add(new SupplyTypes() { SupplyName = "أختر نوع التوريد" });
            supplyTypes.AddRange(await _context.SupplyTypes.ToListAsync());
            ViewData["SupplyTypes"] = new SelectList(supplyTypes, "Id", "SupplyName");
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierNumber,SupplierName,Type,CommercialNumber,TaxId,TaxFileNumber,Address,Phone,Id")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                supplier.create(HttpContext.User.Identity.Name);
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }
            var SupplyName = new List<SupplyTypes>();
            SupplyName.Add(new SupplyTypes() { SupplyName = "أختر نوع التوريد" });
            SupplyName.AddRange(await _context.SupplyTypes.ToListAsync());
            ViewData["SupplyTypes"] = new SelectList(SupplyName, "Id", "SupplyName");
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SupplierNumber,SupplierName,Type,CommercialNumber,TaxId,TaxFileNumber,Address,Phone,Id,CreatedAt,CreatedBy,IsDeleted")] Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    supplier.update(HttpContext.User.Identity.Name);
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplierExists(supplier.Id))
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
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Suppliers == null)
            {
                return NotFound();
            }

            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Suppliers == null)
            {
                return Problem("Entity set 'WebApplication1Context.Supplier'  is null.");
            }
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                supplier.Delete(HttpContext.User.Identity.Name);
                _context.Suppliers.Update(supplier);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplierExists(long id)
        {
            return _context.Suppliers.Any(e => e.Id == id);
        }
    }
}

