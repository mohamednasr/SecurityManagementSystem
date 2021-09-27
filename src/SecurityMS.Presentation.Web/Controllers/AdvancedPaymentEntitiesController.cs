using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class AdvancedPaymentEntitiesController : Controller
    {
        private readonly AppDbContext _context;

        public AdvancedPaymentEntitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AdvancedPaymentEntities
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AdvancedPaymentsEntity.Include(a => a.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AdvancedPaymentEntities/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advancedPaymentEntity = await _context.AdvancedPaymentsEntity
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advancedPaymentEntity == null)
            {
                return NotFound();
            }

            return View(advancedPaymentEntity);
        }

        // GET: AdvancedPaymentEntities/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name");
            return View();
        }

        // POST: AdvancedPaymentEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Amount,installments,IsAcceptable,PayedAt,PayedBy,CreatedBy,CreatedAt,AcceptedBy,AcceptedAt,Id")] AdvancedPaymentEntity advancedPaymentEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advancedPaymentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Employees", new {id = advancedPaymentEntity.EmployeeId });
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", advancedPaymentEntity.EmployeeId);
            return View(advancedPaymentEntity);
        }

        // GET: AdvancedPaymentEntities/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advancedPaymentEntity = await _context.AdvancedPaymentsEntity.FindAsync(id);
            if (advancedPaymentEntity == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", advancedPaymentEntity.EmployeeId);
            return View(advancedPaymentEntity);
        }

        // POST: AdvancedPaymentEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("EmployeeId,Amount,installments,IsAcceptable,PayedAt,PayedBy,CreatedBy,CreatedAt,AcceptedBy,AcceptedAt,Id")] AdvancedPaymentEntity advancedPaymentEntity)
        {
            if (id != advancedPaymentEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advancedPaymentEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvancedPaymentEntityExists(advancedPaymentEntity.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", advancedPaymentEntity.EmployeeId);
            return View(advancedPaymentEntity);
        }

        // GET: AdvancedPaymentEntities/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advancedPaymentEntity = await _context.AdvancedPaymentsEntity
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advancedPaymentEntity == null)
            {
                return NotFound();
            }

            return View(advancedPaymentEntity);
        }

        // POST: AdvancedPaymentEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var advancedPaymentEntity = await _context.AdvancedPaymentsEntity.FindAsync(id);
            _context.AdvancedPaymentsEntity.Remove(advancedPaymentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvancedPaymentEntityExists(long id)
        {
            return _context.AdvancedPaymentsEntity.Any(e => e.Id == id);
        }
    }
}
