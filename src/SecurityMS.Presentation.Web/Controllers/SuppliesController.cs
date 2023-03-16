using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Core.Models;
using SecurityMS.Core.Models.Enums;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
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
            var appDbContext = _context.Supplies
                .Include(s => s.SupplierType);
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
                .Include(s => s.SupplyItems)
                .ThenInclude(i => i.Item)
                .Include(s => s.SupplierType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supply == null)
            {
                return NotFound();
            }

            return View(supply);
        }

        // GET: Supplies/Create
        public async Task<IActionResult> Create()
        {
            var PurchasesIds = new List<SelectModel>();
            PurchasesIds.Add(new SelectModel() { Id = 0, Name = "أختر أمر الشراء" });
            PurchasesIds.AddRange(await _context.Purchases.Where(s => !s.IsDeleted).Select(e => new SelectModel()
            {
                Id = e.Id,
                Name = e.PurchaseCode
            }).ToListAsync());

            ViewData["PurchaseCode"] = new SelectList(PurchasesIds, "Name", "Name");

            ViewData["SupplierTypeId"] = new SelectList(_context.SuppliersTypes, "Id", "Name");

            var SupplyFromIds = new List<SelectModel>();
            SupplyFromIds.Add(new SelectModel() { Id = 0, Name = "أختر جهه التوريد" });
            SupplyFromIds.AddRange(await _context.Suppliers.Where(s => !s.IsDeleted).Select(e => new SelectModel()
            {
                Id = e.Id,
                Name = e.SupplierName
            }).ToListAsync());
            ViewData["SuppliedFromId"] = new SelectList(SupplyFromIds, "Id", "Name");

            Supply _supply = new Supply()
            {
                SupplyCode = (_context.Supplies.Max(s => s.Id) + 1).ToString()
            };

            return View(_supply);
        }

        // POST: Supplies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseCode,SupplyCode,SupplyDate,SupplierTypeId,SuppliedFromId,SuppliedFromName,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supply);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(SuppliedItems), new { Id = supply.Id });
            }

            var PurchasesIds = new List<SelectModel>();
            PurchasesIds.Add(new SelectModel() { Id = 0, Name = "أختر أمر الشراء" });
            PurchasesIds.AddRange(await _context.Purchases.Where(s => !s.IsDeleted).Select(e => new SelectModel()
            {
                Id = e.Id,
                Name = e.PurchaseCode
            }).ToListAsync());

            ViewData["PurchaseCode"] = new SelectList(PurchasesIds, "Id", "Name", supply.PurchaseCode);
            ViewData["SupplierTypeId"] = new SelectList(_context.SuppliersTypes, "Id", "Name", supply.SupplierTypeId);

            var SupplyFromIds = new List<SelectModel>();
            SupplyFromIds.Add(new SelectModel() { Id = 0, Name = "أختر جهه التوريد" });
            SupplyFromIds.AddRange(await _context.Suppliers.Where(s => !s.IsDeleted).Select(e => new SelectModel()
            {
                Id = e.Id,
                Name = e.SupplierName
            }).ToListAsync());
            ViewData["SuppliedFromId"] = new SelectList(SupplyFromIds, "Id", "Name");

            supply.SupplyCode = (_context.Supplies.Max(s => s.Id) + 1).ToString();
            return View(supply);
        }

        public async Task<IActionResult> SuppliedItems(long Id)
        {
            var SupplyEntity = await _context.Supplies.FirstOrDefaultAsync(s => s.Id == Id);
            if (SupplyEntity == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var Items = new List<SelectModel>();
            Items.Add(new SelectModel() { Name = "أختر كود الصنف" });
            Items.AddRange(await _context.Items.Where(x => !x.IsDeleted).Select(i => new SelectModel()
            {
                Id = i.Id,
                Name = i.GetSelectName()
            }).ToListAsync());
            ViewData["Items"] = new SelectList(Items, "Id", "Name");

            if (!string.IsNullOrEmpty(SupplyEntity.PurchaseCode))
            {
                SupplyEntity.SupplyItems = await _context.PurchaseItems.Include(p => p.Purchase).Where(p => p.Purchase.PurchaseCode == SupplyEntity.PurchaseCode).Select(p => new SupplyItems()
                {

                    ItemId = p.ItemId,
                    ItemQuantity = p.Quantity - p.SuppliedQuantity,

                }).ToListAsync();
            }
            return View(SupplyEntity);
        }

        [HttpPost]
        public async Task<bool> SaveSuppliedItems([FromBody] SupplyItemsModal supplyItems)
        {
            try
            {
                var supply = await _context.Supplies.Include(s => s.SupplyItems).FirstOrDefaultAsync(s => s.Id == supplyItems.SupplyId);
                if (supply != null)
                {
                    foreach (var item in supplyItems.Items)
                    {
                        if (!string.IsNullOrEmpty(supply.PurchaseCode))
                        {
                            var suppliedItem = await _context.Items.FirstOrDefaultAsync(i => i.Id == item.ItemId);
                            suppliedItem.AvailableTotalCount += item.ItemQuantity;
                            suppliedItem.TotalCount += item.ItemQuantity;

                            _context.Items.Update(suppliedItem);

                            var purchasedItem = await _context.PurchaseItems.FirstOrDefaultAsync(i => i.ItemId == item.ItemId);
                            purchasedItem.SuppliedQuantity += item.ItemQuantity;

                            _context.PurchaseItems.Update(purchasedItem);
                        }
                        supply.SupplyItems.Add(new SupplyItems()
                        {
                            ItemId = item.ItemId,
                            ItemQuantity = item.ItemQuantity,
                            SupplyId = item.SupplyId
                        });
                    }
                    _context.Update(supply);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            ViewData["PurchaseCode"] = new SelectList(_context.Purchases, "Id", "PurchaseCode", supply.PurchaseCode);
            ViewData["SupplierTypeId"] = new SelectList(_context.SuppliersTypes, "Id", "Name", supply.SupplierTypeId);
            return View(supply);
        }

        // POST: Supplies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PurchaseCode,SupplyCode,SupplyDate,SupplierTypeId,SuppliedFromId,SuppliedFromName,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted")] Supply supply)
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
            ViewData["PurchaseCode"] = new SelectList(_context.Purchases, "PurchaseCode", "PurchaseCode", supply.PurchaseCode);
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
                //.Include(s => s.Purchase)
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

        public async Task<List<SelectModel>> GetsupplyFromIds(int id)
        {
            switch (id)
            {
                case (int)SupplierTypeEnum.Supplier:
                    return await _context.Suppliers.Where(s => !s.IsDeleted).Select(e => new SelectModel()
                    {
                        Id = e.Id,
                        Name = e.SupplierName
                    }).ToListAsync();
                case (int)SupplierTypeEnum.Site:
                    return await _context.SitesEntities.Where(s => !s.IsDeleted).Select(e => new SelectModel()
                    {
                        Id = e.Id,
                        Name = e.Name
                    }).ToListAsync();
                case (int)SupplierTypeEnum.Personal:
                    return await _context.EmployeesEntities.Where(s => !s.IsDeleted).Select(e => new SelectModel()
                    {
                        Id = e.Id,
                        Name = e.Name
                    }).ToListAsync();
                default:
                    return new List<SelectModel>();

            }
        }

        [HttpGet]
        public async Task<IActionResult> AddNewSuppliedItem()
        {
            var Items = new List<SelectModel>();
            Items.Add(new SelectModel() { Name = "أختر كود الصنف" });
            Items.AddRange(await _context.Items.Where(x => !x.IsDeleted).Select(i => new SelectModel()
            {
                Id = i.Id,
                Name = i.GetSelectName()
            }).ToListAsync());
            ViewData["Items"] = new SelectList(Items, "Id", "Name");

            return PartialView("_SuppliedItemCreate");
        }

        private bool SupplyExists(long id)
        {
            return _context.Supplies.Any(e => e.Id == id);
        }
    }
}
