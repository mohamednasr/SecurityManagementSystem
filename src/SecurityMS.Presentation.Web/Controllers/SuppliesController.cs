﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class SuppliesController : Controller
    {
        private readonly AppDbContext _context;

        public SuppliesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Supplies
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Supplies.Include(s => s.Purchase).Include(s => s.SupplierType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Supplies/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Supplies == null)
            {
                return NotFound();
            }

            var supply = await _context.Supplies
                .Include(s => s.Purchase)
                .Include(s => s.SupplierType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supply == null)
            {
                return NotFound();
            }

            return View(supply);
        }

        // GET: Supplies/Create
        public IActionResult Create()
        {
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "Id", "PurchaseCode");
            ViewData["SupplierTypeId"] = new SelectList(_context.SuppliersTypes, "Id", "Name");
            return View();
        }

        // POST: Supplies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseId,SupplyCode,SupplyDate,SupplierTypeId,SuppliedFromId,SuppliedFromName,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supply);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "Id", "PurchaseCode", supply.PurchaseId);
            ViewData["SupplierTypeId"] = new SelectList(_context.SuppliersTypes, "Id", "Name", supply.SupplierTypeId);
            return View(supply);
        }

        // GET: Supplies/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Supplies == null)
            {
                return NotFound();
            }

            var supply = await _context.Supplies.FindAsync(id);
            if (supply == null)
            {
                return NotFound();
            }
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "Id", "PurchaseCode", supply.PurchaseId);
            ViewData["SupplierTypeId"] = new SelectList(_context.SuppliersTypes, "Id", "Name", supply.SupplierTypeId);
            return View(supply);
        }

        // POST: Supplies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PurchaseId,SupplyCode,SupplyDate,SupplierTypeId,SuppliedFromId,SuppliedFromName,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted")] Supply supply)
        {
            if (id != supply.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supply);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupplyExists(supply.Id))
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
            ViewData["PurchaseId"] = new SelectList(_context.Purchases, "Id", "PurchaseCode", supply.PurchaseId);
            ViewData["SupplierTypeId"] = new SelectList(_context.SuppliersTypes, "Id", "Name", supply.SupplierTypeId);
            return View(supply);
        }

        // GET: Supplies/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Supplies == null)
            {
                return NotFound();
            }

            var supply = await _context.Supplies
                .Include(s => s.Purchase)
                .Include(s => s.SupplierType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supply == null)
            {
                return NotFound();
            }

            return View(supply);
        }

        // POST: Supplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Supplies == null)
            {
                return Problem("Entity set 'AppDbContext.Supplies'  is null.");
            }
            var supply = await _context.Supplies.FindAsync(id);
            if (supply != null)
            {
                _context.Supplies.Remove(supply);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupplyExists(long id)
        {
            return _context.Supplies.Any(e => e.Id == id);
        }
    }
}
