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
    public class RewardsController : Controller
    {
        private readonly AppDbContext _context;

        public RewardsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Rewards
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RewardsEntity.Include(r => r.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Rewards/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rewardEntity = await _context.RewardsEntity
                .Include(r => r.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rewardEntity == null)
            {
                return NotFound();
            }

            return View(rewardEntity);
        }

        // GET: Rewards/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name");
            return View();
        }

        // POST: Rewards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,RewardType,Amount,Reason,Id, RewardDate")] RewardEntity rewardEntity)
        {
            if (ModelState.IsValid)
            {
                rewardEntity.create(HttpContext.User.Identity.Name);
                rewardEntity.RewardValue = rewardEntity.RewardType == (int)RewardTypeEnum.Days ? await GetRewardValue(rewardEntity.EmployeeId, rewardEntity.Amount) : rewardEntity.Amount;
                _context.Add(rewardEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Employees", new { id = rewardEntity.EmployeeId });
                //return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", rewardEntity.EmployeeId);
            return View(rewardEntity);
        }

        private async Task<decimal> GetRewardValue(long EmployeeId, decimal value)
        {
            var employee = await _context.SiteEmployeesAssignEntities.Include(s => s.SiteEmployee).FirstOrDefaultAsync(x => x.EmployeeId == EmployeeId);
            if (employee != null)
            {
                return value * employee.EmployeeSalary;
            }
            return 0;
        }
        // GET: Rewards/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rewardEntity = await _context.RewardsEntity.FindAsync(id);
            if (rewardEntity == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", rewardEntity.EmployeeId);
            return View(rewardEntity);
        }

        // POST: Rewards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("EmployeeId,RewardType,Amount,Reason,Id")] RewardEntity rewardEntity)
        {
            if (id != rewardEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rewardEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RewardEntityExists(rewardEntity.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", rewardEntity.EmployeeId);
            return View(rewardEntity);
        }

        // GET: Rewards/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rewardEntity = await _context.RewardsEntity
                .Include(r => r.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rewardEntity == null)
            {
                return NotFound();
            }

            return View(rewardEntity);
        }

        // POST: Rewards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var rewardEntity = await _context.RewardsEntity.FindAsync(id);
            _context.RewardsEntity.Remove(rewardEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RewardEntityExists(long id)
        {
            return _context.RewardsEntity.Any(e => e.Id == id);
        }
    }
}
