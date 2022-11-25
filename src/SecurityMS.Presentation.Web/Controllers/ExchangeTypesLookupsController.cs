using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class ExchangeTypesLookupsController : Controller
    {
        private readonly AppDbContext _context;

        public ExchangeTypesLookupsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ExchangeTypesLookups
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExchangeTypesLookup.Where(e => !e.IsDeleted).ToListAsync());
        }

        // GET: ExchangeTypesLookups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ExchangeTypesLookup == null)
            {
                return NotFound();
            }

            var exchangeTypesLookups = await _context.ExchangeTypesLookup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exchangeTypesLookups == null)
            {
                return NotFound();
            }

            return View(exchangeTypesLookups);
        }

        // GET: ExchangeTypesLookups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExchangeTypesLookups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted")] ExchangeTypesLookups exchangeTypesLookups)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exchangeTypesLookups);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exchangeTypesLookups);
        }

        // GET: ExchangeTypesLookups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ExchangeTypesLookup == null)
            {
                return NotFound();
            }

            var exchangeTypesLookups = await _context.ExchangeTypesLookup.FindAsync(id);
            if (exchangeTypesLookups == null)
            {
                return NotFound();
            }
            return View(exchangeTypesLookups);
        }

        // POST: ExchangeTypesLookups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,IsDeleted")] ExchangeTypesLookups exchangeTypesLookups)
        {
            if (id != exchangeTypesLookups.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exchangeTypesLookups);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExchangeTypesLookupsExists(exchangeTypesLookups.Id))
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
            return View(exchangeTypesLookups);
        }

        // GET: ExchangeTypesLookups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ExchangeTypesLookup == null)
            {
                return NotFound();
            }

            var exchangeTypesLookups = await _context.ExchangeTypesLookup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exchangeTypesLookups == null)
            {
                return NotFound();
            }

            return View(exchangeTypesLookups);
        }

        // POST: ExchangeTypesLookups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ExchangeTypesLookup == null)
            {
                return Problem("Entity set 'AppDbContext.ExchangeTypesLookup'  is null.");
            }
            var exchangeTypesLookups = await _context.ExchangeTypesLookup.FindAsync(id);
            if (exchangeTypesLookups != null)
            {
                exchangeTypesLookups.Delete(HttpContext.User.Identity.Name);
                _context.ExchangeTypesLookup.Update(exchangeTypesLookups);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExchangeTypesLookupsExists(int id)
        {
            return _context.ExchangeTypesLookup.Any(e => e.Id == id);
        }
    }
}
