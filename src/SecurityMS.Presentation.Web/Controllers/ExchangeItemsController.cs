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
    public class ExchangeItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ExchangeItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ExchangeItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ExhangeItems.Include(e => e.Exchange).Include(e => e.Item);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ExchangeItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.ExhangeItems == null)
            {
                return NotFound();
            }

            var exchangeItems = await _context.ExhangeItems
                .Include(e => e.Exchange)
                .Include(e => e.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exchangeItems == null)
            {
                return NotFound();
            }

            return View(exchangeItems);
        }

        // GET: ExchangeItems/Create
        public IActionResult Create()
        {
            ViewData["ExchangeId"] = new SelectList(_context.ExchangeEntity, "Id", "Id");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id");
            return View();
        }

        // POST: ExchangeItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExchangeId,ItemId,ItemQuantity")] ExchangeItems exchangeItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exchangeItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExchangeId"] = new SelectList(_context.ExchangeEntity, "Id", "Id", exchangeItems.ExchangeId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id", exchangeItems.ItemId);
            return View(exchangeItems);
        }

        // GET: ExchangeItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.ExhangeItems == null)
            {
                return NotFound();
            }

            var exchangeItems = await _context.ExhangeItems.FindAsync(id);
            if (exchangeItems == null)
            {
                return NotFound();
            }
            ViewData["ExchangeId"] = new SelectList(_context.ExchangeEntity, "Id", "Id", exchangeItems.ExchangeId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id", exchangeItems.ItemId);
            return View(exchangeItems);
        }

        // POST: ExchangeItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ExchangeId,ItemId,ItemQuantity")] ExchangeItems exchangeItems)
        {
            if (id != exchangeItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exchangeItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExchangeItemsExists(exchangeItems.Id))
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
            ViewData["ExchangeId"] = new SelectList(_context.ExchangeEntity, "Id", "Id", exchangeItems.ExchangeId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Id", exchangeItems.ItemId);
            return View(exchangeItems);
        }

        // GET: ExchangeItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.ExhangeItems == null)
            {
                return NotFound();
            }

            var exchangeItems = await _context.ExhangeItems
                .Include(e => e.Exchange)
                .Include(e => e.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exchangeItems == null)
            {
                return NotFound();
            }

            return View(exchangeItems);
        }

        // POST: ExchangeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.ExhangeItems == null)
            {
                return Problem("Entity set 'AppDbContext.ExhangeItems'  is null.");
            }
            var exchangeItems = await _context.ExhangeItems.FindAsync(id);
            if (exchangeItems != null)
            {
                _context.ExhangeItems.Remove(exchangeItems);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExchangeItemsExists(long id)
        {
          return _context.ExhangeItems.Any(e => e.Id == id);
        }
    }
}
