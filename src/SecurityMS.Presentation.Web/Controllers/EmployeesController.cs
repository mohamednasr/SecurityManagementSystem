using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EmployeesEntities.Include(e => e.Job);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesEntity = await _context.EmployeesEntities
                .Include(e => e.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeesEntity == null)
            {
                return NotFound();
            }

            return View(employeesEntity);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            var jobs = new List<JobsEntity>();
            jobs.Add(new JobsEntity() { Id = 0, Name = "أختر الوظيفة" });
            jobs.AddRange(await _context.JobsEntities.ToListAsync());
            ViewData["JobId"] = new SelectList(jobs, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,EmployeeCode,NationalId,Phone,Phone2,StartDate,EndDate,InsuranceNumber,JobId,Id")] EmployeesEntity employeesEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeesEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var jobs = new List<JobsEntity>();
            jobs.Add(new JobsEntity() { Id = 0, Name = "أختر الوظيفة" });
            jobs.AddRange(await _context.JobsEntities.ToListAsync());
            ViewData["JobId"] = new SelectList(jobs, "Id", "Name", employeesEntity.JobId);
            return View(employeesEntity);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesEntity = await _context.EmployeesEntities.FindAsync(id);
            if (employeesEntity == null)
            {
                return NotFound();
            }
            var jobs = new List<JobsEntity>();
            jobs.Add(new JobsEntity() { Id = 0, Name = "أختر الوظيفة" });
            jobs.AddRange(await _context.JobsEntities.ToListAsync());
            ViewData["JobId"] = new SelectList(jobs, "Id", "Name", employeesEntity.JobId);
            return View(employeesEntity);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,EmployeeCode,NationalId,Phone,Phone2,StartDate,EndDate,InsuranceNumber,JobId,Id")] EmployeesEntity employeesEntity)
        {
            if (id != employeesEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeesEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesEntityExists(employeesEntity.Id))
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
            var jobs = new List<JobsEntity>();
            jobs.Add(new JobsEntity() { Id = 0, Name = "أختر الوظيفة" });
            jobs.AddRange(await _context.JobsEntities.ToListAsync());
            ViewData["JobId"] = new SelectList(jobs, "Id", "Name", employeesEntity.JobId);
            return View(employeesEntity);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesEntity = await _context.EmployeesEntities
                .Include(e => e.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeesEntity == null)
            {
                return NotFound();
            }

            return View(employeesEntity);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var employeesEntity = await _context.EmployeesEntities.FindAsync(id);
            _context.EmployeesEntities.Remove(employeesEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeesEntityExists(long id)
        {
            return _context.EmployeesEntities.Any(e => e.Id == id);
        }
    }
}
