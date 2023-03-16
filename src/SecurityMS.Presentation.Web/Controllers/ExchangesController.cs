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
    public class ExchangesController : Controller
    {
        private readonly AppDbContext _context;

        public ExchangesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Exchanges
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ExchangeEntity.Include(e => e.ExchangeType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Exchanges/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.ExchangeEntity == null)
            {
                return NotFound();
            }

            var exchangeEntity = await _context.ExchangeEntity
                .Include(e => e.ExchangeType)
                .Include(e => e.ExchangeItems)
                .ThenInclude(i => i.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exchangeEntity == null)
            {
                return NotFound();
            }

            return View(exchangeEntity);
        }

        // GET: Exchanges/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ExchangeTypeId"] = new SelectList(_context.ExchangeTypesLookup, "Id", "Name");
            ViewData["Items"] = new SelectList(await _context.Items.Select(i => new SelectModel()
            {
                Id = i.Id,
                Name = i.GetSelectName()
            }).ToListAsync(), "Id", "Name");

            var ExchangeTargetIds = new List<SelectModel>();
            ExchangeTargetIds.Add(new SelectModel() { Id = 0, Name = "أختر جهه الصرف" });
            ExchangeTargetIds.AddRange(await _context.EmployeesEntities.Select(e => new SelectModel()
            {
                Id = e.Id,
                Name = e.Name
            }).ToListAsync());
            ViewData["ExchangeToIds"] = new SelectList(ExchangeTargetIds, "Id", "Name");

            ExchangeEntity exchangeEntity = new ExchangeEntity()
            {
                ExchangeDate = DateTime.Now,
                ExchangeItems = new List<ExchangeItems>() { new ExchangeItems() }
            };
            return View(exchangeEntity);
        }

        // POST: Exchanges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExchangeDate,ExchangeTypeId,ExchangeTo,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted")] ExchangeEntity exchangeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exchangeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExchangeTypeId"] = new SelectList(_context.ExchangeTypesLookup, "Id", "Name");
            ViewData["Items"] = new SelectList(await _context.Items.Select(i => new SelectModel()
            {
                Id = i.Id,
                Name = i.GetSelectName()
            }).ToListAsync(), "Id", "Name");

            var ExchangeTargetIds = new List<SelectModel>();
            ExchangeTargetIds.Add(new SelectModel() { Id = 0, Name = "أختر جهه الصرف" });
            ExchangeTargetIds.AddRange(await _context.EmployeesEntities.Select(e => new SelectModel()
            {
                Id = e.Id,
                Name = e.Name
            }).ToListAsync());
            ViewData["ExchangeToIds"] = new SelectList(ExchangeTargetIds, "Id", "Name");

            return View(exchangeEntity);
        }

        // GET: Exchanges/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.ExchangeEntity == null)
            {
                return NotFound();
            }

            var exchangeEntity = await _context.ExchangeEntity.FindAsync(id);
            if (exchangeEntity == null)
            {
                return NotFound();
            }
            ViewData["ExchangeTypeId"] = new SelectList(_context.ExchangeTypesLookup, "Id", "Name", exchangeEntity.ExchangeTypeId);

            return View(exchangeEntity);
        }

        // POST: Exchanges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ExchangeDate,ExchangeTypeId,ExchangeTo,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted")] ExchangeEntity exchangeEntity)
        {
            if (id != exchangeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exchangeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExchangeEntityExists(exchangeEntity.Id))
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
            ViewData["ExchangeTypeId"] = new SelectList(_context.ExchangeTypesLookup, "Id", "Name", exchangeEntity.ExchangeTypeId);
            return View(exchangeEntity);
        }

        // GET: Exchanges/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.ExchangeEntity == null)
            {
                return NotFound();
            }

            var exchangeEntity = await _context.ExchangeEntity
                .Include(e => e.ExchangeType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exchangeEntity == null)
            {
                return NotFound();
            }

            return View(exchangeEntity);
        }

        // POST: Exchanges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.ExchangeEntity == null)
            {
                return Problem("Entity set 'AppDbContext.ExchangeEntity'  is null.");
            }
            var exchangeEntity = await _context.ExchangeEntity.FindAsync(id);
            if (exchangeEntity != null)
            {
                exchangeEntity.Delete(HttpContext.User.Identity.Name);
                _context.ExchangeEntity.Update(exchangeEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddNewItem(string ItemCount)
        {
            ViewData["ItemsCount"] = int.Parse(ItemCount) + 1;
            ViewData["Items"] = new SelectList(await _context.Items.Select(i => new SelectModel()
            {
                Id = i.Id,
                Name = i.GetSelectName()
            }).ToListAsync(), "Id", "Name");
            ExchangeItems newItem = new ExchangeItems();
            return PartialView("_ExchangedItemCreate", newItem);
        }

        [HttpPost]
        public async Task<bool> SaveExchange([FromBody] ExchangeModel Exchange)
        {
            try
            {
                ExchangeEntity ExchangeData = new ExchangeEntity()
                {
                    ExchangeDate = Exchange.ExchangeDate,
                    ExchangeTo = Exchange.ExchangeTo.GetValueOrDefault(0) == 0 ? null : Exchange.ExchangeTo.Value,
                    ExchangeName = Exchange.ExchangeName,
                    ExchangeTypeId = Exchange.ExchangeType,
                    ExchangeItems = new List<ExchangeItems>()
                };
                ExchangeData.create(HttpContext.User.Identity.Name);
                var addedItemsIds = Exchange.ExchangeItems.Select(s => s.ItemId).ToList();

                var selectedItems = await _context.Items.Where(i => addedItemsIds.Contains(i.Id)).ToListAsync();
                try
                {
                    Exchange.ExchangeItems.ForEach(e =>
                    {
                        ExchangeItems ExchangeItem = new ExchangeItems()
                        {
                            ItemId = e.ItemId,
                            ItemQuantity = e.Quantity
                        };
                        var SelectedItem = selectedItems.FirstOrDefault(s => s.Id == e.ItemId);
                        if (SelectedItem.AvailableTotalCount < e.Quantity)
                        {
                            throw new Exception("الكمية المطلوبة غير متوفره");
                        }
                        SelectedItem.AvailableTotalCount = SelectedItem.AvailableTotalCount - e.Quantity;
                        if (ExchangeData.ExchangeTypeId == (int)ExchangeTypeEnum.Destroyed)
                        {
                            SelectedItem.TotalCount = SelectedItem.TotalCount - e.Quantity;
                        }
                        _context.Items.Update(SelectedItem);
                        ExchangeData.ExchangeItems.Add(ExchangeItem);
                    });
                }
                catch (Exception ex)
                {
                    return false;
                }
                _context.ExchangeEntity.Attach(ExchangeData);
                await _context.SaveChangesAsync();
                return true; //RedirectToAction("Details", "Sites", new { id = attendance.SiteId });
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<List<SelectModel>> GetExchangeTargetIds(int id)
        {
            switch (id)
            {
                case (int)ExchangeTypeEnum.Personal:
                    return await _context.EmployeesEntities.Where(s => !s.IsDeleted).Select(e => new SelectModel()
                    {
                        Id = e.Id,
                        Name = e.Name
                    }).ToListAsync();
                case (int)ExchangeTypeEnum.Site:
                    return await _context.SitesEntities.Where(s => !s.IsDeleted).Select(e => new SelectModel()
                    {
                        Id = e.Id,
                        Name = e.Name
                    }).ToListAsync();
                default:
                    return new List<SelectModel>();

            }
        }

        private bool ExchangeEntityExists(long id)
        {
            return _context.ExchangeEntity.Any(e => e.Id == id);
        }
    }
}
