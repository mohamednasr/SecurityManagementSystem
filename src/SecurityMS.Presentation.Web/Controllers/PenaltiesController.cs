using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Core.Models.Enums;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class PenaltiesController : Controller
    {
        private readonly AppDbContext _context;

        public PenaltiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Penalties
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PenaltiesEntity.Include(p => p.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Penalties/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penaltyEntity = await _context.PenaltiesEntity
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penaltyEntity == null)
            {
                return NotFound();
            }

            return View(penaltyEntity);
        }

        // GET: Penalties/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name");
            return View();
        }

        // POST: Penalties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,PenaltyType,Amount,Reason,Id, PenalityDate")] PenaltyEntity penaltyEntity)
        {
            if (ModelState.IsValid)
            {
                penaltyEntity.PenaltyValue = penaltyEntity.PenaltyType == (int)PenalityTypeEnum.Days ? await GetPenalityValue(penaltyEntity.EmployeeId, penaltyEntity.Amount) : penaltyEntity.Amount;
                penaltyEntity.create(HttpContext.User.Identity.Name);
                _context.Add(penaltyEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Employees", new { id = penaltyEntity.EmployeeId });
                //return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", penaltyEntity.EmployeeId);
            return View(penaltyEntity);
        }
        private async Task<decimal> GetPenalityValue(long EmployeeId, decimal value)
        {
            var employee = await _context.SiteEmployeesAssignEntities.Include(s => s.SiteEmployee).FirstOrDefaultAsync(x => x.EmployeeId == EmployeeId);
            if (employee != null)
            {
                return value * employee.EmployeeSalary;
            }
            return 0;
        }
        // GET: Penalties/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penaltyEntity = await _context.PenaltiesEntity.FindAsync(id);
            if (penaltyEntity == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", penaltyEntity.EmployeeId);
            return View(penaltyEntity);
        }

        // POST: Penalties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("EmployeeId,PenaltyType,Amount,Reason,Id")] PenaltyEntity penaltyEntity)
        {
            if (id != penaltyEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penaltyEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenaltyEntityExists(penaltyEntity.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", penaltyEntity.EmployeeId);
            return View(penaltyEntity);
        }

        // GET: Penalties/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var penaltyEntity = await _context.PenaltiesEntity
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (penaltyEntity == null)
            {
                return NotFound();
            }

            return View(penaltyEntity);
        }

        // POST: Penalties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var penaltyEntity = await _context.PenaltiesEntity.FindAsync(id);
            _context.PenaltiesEntity.Remove(penaltyEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenaltyEntityExists(long id)
        {
            return _context.PenaltiesEntity.Any(e => e.Id == id);
        }
    }
}
