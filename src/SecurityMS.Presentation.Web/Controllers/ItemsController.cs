using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.Include(x => x.SupplyType).ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemEntity = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemEntity == null)
            {
                return NotFound();
            }

            return View(itemEntity);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name,TotalCount,AvailableTotalCount,MinimumAlert,Id")] ItemEntity itemEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemEntity);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemEntity = await _context.Items.FindAsync(id);
            if (itemEntity == null)
            {
                return NotFound();
            }
            return View(itemEntity);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Code,Name,TotalCount,AvailableTotalCount,MinimumAlert,Id")] ItemEntity itemEntity)
        {
            if (id != itemEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemEntityExists(itemEntity.Id))
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
            return View(itemEntity);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemEntity = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemEntity == null)
            {
                return NotFound();
            }

            return View(itemEntity);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var itemEntity = await _context.Items.FindAsync(id);
            _context.Items.Remove(itemEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemEntityExists(long id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
