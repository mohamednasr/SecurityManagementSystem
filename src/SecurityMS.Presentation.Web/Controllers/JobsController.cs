using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class JobsController : Controller
    {
        private readonly AppDbContext _context;

        public JobsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.JobsEntities.Include(j => j.Department);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobsEntity = await _context.JobsEntities
                .Include(j => j.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobsEntity == null)
            {
                return NotFound();
            }

            return View(jobsEntity);
        }

        // GET: Jobs/Create
        public async Task<IActionResult> Create()
        {
            var Departments = new List<DepartmentsEntity>();
            Departments.Add(new DepartmentsEntity() { Name = "أختر القسم" });
            Departments.AddRange(await _context.DepartmentsEntities.ToListAsync());
            ViewData["DepartmentId"] = new SelectList(Departments, "Id", "Name");

            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DepartmentId,Id")] JobsEntity jobsEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobsEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var Departments = new List<DepartmentsEntity>();
            Departments.Add(new DepartmentsEntity() { Name = "أختر القسم" });
            Departments.AddRange(await _context.DepartmentsEntities.ToListAsync());
            ViewData["DepartmentId"] = new SelectList(Departments, "Id", "Name", jobsEntity.DepartmentId);
            return View(jobsEntity);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobsEntity = await _context.JobsEntities.FindAsync(id);
            if (jobsEntity == null)
            {
                return NotFound();
            }
            var Departments = new List<DepartmentsEntity>();
            Departments.Add(new DepartmentsEntity() { Name = "أختر القسم" });
            Departments.AddRange(await _context.DepartmentsEntities.ToListAsync());
            ViewData["DepartmentId"] = new SelectList(Departments, "Id", "Name", jobsEntity.DepartmentId);
            return View(jobsEntity);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,DepartmentId,Id")] JobsEntity jobsEntity)
        {
            if (id != jobsEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && jobsEntity.DepartmentId > 0)
            {
                try
                {
                    _context.Update(jobsEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobsEntityExists(jobsEntity.Id))
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
            var Departments = new List<DepartmentsEntity>();
            Departments.Add(new DepartmentsEntity() { Name = "أختر القسم" });
            Departments.AddRange(await _context.DepartmentsEntities.ToListAsync());
            ViewData["DepartmentId"] = new SelectList(Departments, "Id", "Name", jobsEntity.DepartmentId);
            return View(jobsEntity);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobsEntity = await _context.JobsEntities
                .Include(j => j.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobsEntity == null)
            {
                return NotFound();
            }

            return View(jobsEntity);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var jobsEntity = await _context.JobsEntities.FindAsync(id);
            _context.JobsEntities.Remove(jobsEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobsEntityExists(long id)
        {
            return _context.JobsEntities.Any(e => e.Id == id);
        }
    }
}
