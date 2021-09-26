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
    public class SiteEmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public SiteEmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SiteEmployees
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SiteEmployeesEntities.Include(s => s.Job).Include(s => s.ShiftType).Include(s => s.Site);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SiteEmployees/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEmployeesEntity = await _context.SiteEmployeesEntities
                .Include(s => s.Job)
                .Include(s => s.ShiftType)
                .Include(s => s.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteEmployeesEntity == null)
            {
                return NotFound();
            }

            return View(siteEmployeesEntity);
        }

        // GET: SiteEmployees/Create
        public IActionResult Create()
        {
            ViewData["JobId"] = new SelectList(_context.JobsEntities, "Id", "Name");
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypesLookups, "Id", "Name");
            ViewData["SiteId"] = new SelectList(_context.SitesEntities, "Id", "Name");
            return View();
        }


        public IActionResult CreateSiteEmployees(long? id)
        {
            SiteEmployeesEntity siteEmployee = new SiteEmployeesEntity();
            siteEmployee.SiteId = id.Value;
            ViewData["JobId"] = new SelectList(_context.JobsEntities.Where(x => x.DepartmentId == (int)DepartmentsEnum.Operations).ToList(), "Id", "Name");
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypesLookups, "Id", "Name");
            return View("Create", siteEmployee);
        }


        // POST: SiteEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiteId,JobId,ShiftTypeId,ShiftValue,EmployeeShiftSalary,EmployeesPerShift,Id")] SiteEmployeesEntity siteEmployeesEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siteEmployeesEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.JobsEntities.Where(x => x.DepartmentId == (int)DepartmentsEnum.Operations).ToList(), "Id", "Name", siteEmployeesEntity.JobId);
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypesLookups, "Id", "Name", siteEmployeesEntity.ShiftTypeId);
            ViewData["SiteId"] = new SelectList(_context.SitesEntities, "Id", "Name", siteEmployeesEntity.SiteId);
            return View(siteEmployeesEntity);
        }

        // GET: SiteEmployees/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEmployeesEntity = await _context.SiteEmployeesEntities.FindAsync(id);
            if (siteEmployeesEntity == null)
            {
                return NotFound();
            }
            ViewData["JobId"] = new SelectList(_context.JobsEntities.Where(x => x.DepartmentId == (int)DepartmentsEnum.Operations).ToList(), "Id", "Name", siteEmployeesEntity.JobId);
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypesLookups, "Id", "Name", siteEmployeesEntity.ShiftTypeId);
            ViewData["SiteId"] = new SelectList(_context.SitesEntities, "Id", "Name", siteEmployeesEntity.SiteId);
            return View(siteEmployeesEntity);
        }

        // POST: SiteEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SiteId,JobId,ShiftTypeId,ShiftValue,EmployeeShiftSalary,EmployeesPerShift,Id")] SiteEmployeesEntity siteEmployeesEntity)
        {
            if (id != siteEmployeesEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteEmployeesEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteEmployeesEntityExists(siteEmployeesEntity.Id))
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
            ViewData["JobId"] = new SelectList(_context.JobsEntities.Where(x => x.DepartmentId == (int)DepartmentsEnum.Operations).ToList(), "Id", "Name", siteEmployeesEntity.JobId);
            ViewData["ShiftTypeId"] = new SelectList(_context.ShiftTypesLookups, "Id", "Name", siteEmployeesEntity.ShiftTypeId);
            ViewData["SiteId"] = new SelectList(_context.SitesEntities, "Id", "Name", siteEmployeesEntity.SiteId);
            return View(siteEmployeesEntity);
        }

        // GET: SiteEmployees/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEmployeesEntity = await _context.SiteEmployeesEntities
                .Include(s => s.Job)
                .Include(s => s.ShiftType)
                .Include(s => s.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteEmployeesEntity == null)
            {
                return NotFound();
            }

            return View(siteEmployeesEntity);
        }

        // POST: SiteEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var siteEmployeesEntity = await _context.SiteEmployeesEntities.FindAsync(id);
            _context.SiteEmployeesEntities.Remove(siteEmployeesEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteEmployeesEntityExists(long id)
        {
            return _context.SiteEmployeesEntities.Any(e => e.Id == id);
        }
    }
}
