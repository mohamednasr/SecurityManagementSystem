using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class IncomeTaxesMatricesController : Controller
    {
        private readonly AppDbContext _context;

        public IncomeTaxesMatricesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: IncomeTaxesMatrices
        public async Task<IActionResult> Index()
        {
            return View(await _context.IncomeTaxesMatrix.ToListAsync());
        }

        // GET: IncomeTaxesMatrices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IncomeTaxesMatrix == null)
            {
                return NotFound();
            }

            var incomeTaxesMatrix = await _context.IncomeTaxesMatrix
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomeTaxesMatrix == null)
            {
                return NotFound();
            }

            return View(incomeTaxesMatrix);
        }

        // GET: IncomeTaxesMatrices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IncomeTaxesMatrices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RangeFrom,RangeTo,TaxesPercentage,TaxesExemption")] IncomeTaxesMatrix incomeTaxesMatrix)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incomeTaxesMatrix);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(incomeTaxesMatrix);
        }

        // GET: IncomeTaxesMatrices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IncomeTaxesMatrix == null)
            {
                return NotFound();
            }

            var incomeTaxesMatrix = await _context.IncomeTaxesMatrix.FindAsync(id);
            if (incomeTaxesMatrix == null)
            {
                return NotFound();
            }
            return View(incomeTaxesMatrix);
        }

        // POST: IncomeTaxesMatrices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RangeFrom,RangeTo,TaxesPercentage,TaxesExemption")] IncomeTaxesMatrix incomeTaxesMatrix)
        {
            if (id != incomeTaxesMatrix.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incomeTaxesMatrix);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncomeTaxesMatrixExists(incomeTaxesMatrix.Id))
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
            return View(incomeTaxesMatrix);
        }

        // GET: IncomeTaxesMatrices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IncomeTaxesMatrix == null)
            {
                return NotFound();
            }

            var incomeTaxesMatrix = await _context.IncomeTaxesMatrix
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incomeTaxesMatrix == null)
            {
                return NotFound();
            }

            return View(incomeTaxesMatrix);
        }

        // POST: IncomeTaxesMatrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IncomeTaxesMatrix == null)
            {
                return Problem("Entity set 'AppDbContext.IncomeTaxesMatrix'  is null.");
            }
            var incomeTaxesMatrix = await _context.IncomeTaxesMatrix.FindAsync(id);
            if (incomeTaxesMatrix != null)
            {
                _context.IncomeTaxesMatrix.Remove(incomeTaxesMatrix);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncomeTaxesMatrixExists(int id)
        {
            return _context.IncomeTaxesMatrix.Any(e => e.Id == id);
        }
    }
}
