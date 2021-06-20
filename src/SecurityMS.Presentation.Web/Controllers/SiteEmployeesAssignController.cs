using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Core.Models;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class SiteEmployeesAssignController : Controller
    {
        private readonly AppDbContext _context;

        public SiteEmployeesAssignController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SiteEmployeesAssign
        public async Task<IActionResult> Index(long id)
        {
            //if (id > 0)
            //{
                var appDbContext = await _context.SiteEmployeesAssignEntities.Include(s => s.Employee).Include(s => s.SiteEmployee).Where(s => s.SiteEmployeeId == id).ToListAsync();
            SiteEmployeesAssignModel siteEmployees = new SiteEmployeesAssignModel()
            {
                SiteEmployeeId = id,
                SiteEmployee = await _context.SiteEmployeesEntities.Include(s => s.Site).Where(s => s.Id == id).FirstOrDefaultAsync(),
                SiteAssignedEmployees = appDbContext.Select(s => new SiteEmployeesAssignListModel()
                {
                    Id = s.Id,
                    EmployeeId = s.EmployeeId,
                    Employee = s.Employee,
                    EmployeeShiftSalary = s.EmployeeShiftSalary,
                    IsActive = s.IsActive
                }).ToList()
            };
                return View(siteEmployees);

            //}
            //else
            //{
            //    var appDbContext = await _context.SiteEmployeesAssignEntities.Include(s => s.Employee).Include(s => s.SiteEmployee).ThenInclude(s => s.Site).ToListAsync();
            //    return View(appDbContext);
            //}
        }
        // GET: SiteEmployeesAssign/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEmployeesAssignEntity = await _context.SiteEmployeesAssignEntities
                .Include(s => s.Employee)
                .Include(s => s.SiteEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteEmployeesAssignEntity == null)
            {
                return NotFound();
            }

            return View(siteEmployeesAssignEntity);
        }

        // GET: SiteEmployeesAssign/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name");
            ViewData["SiteEmployeeId"] = new SelectList(_context.SiteEmployeesEntities, "Id", "Name");
            return View();
        }
        public IActionResult AssignSiteEmployee(long id)
        {
            SiteEmployeesAssignEntity siteEmployee = new SiteEmployeesAssignEntity()
            {
                SiteEmployeeId = id
            };
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name");
            ViewData["SiteEmployeeId"] = new SelectList(_context.SiteEmployeesEntities, "Id", "Name");
            return View("Create", siteEmployee);
        }
        // POST: SiteEmployeesAssign/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiteEmployeeId,EmployeeId,EmployeeShiftSalary,IsActive,Id")] SiteEmployeesAssignEntity siteEmployeesAssignEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siteEmployeesAssignEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = siteEmployeesAssignEntity.SiteEmployeeId });
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", siteEmployeesAssignEntity.EmployeeId);
            ViewData["SiteEmployeeId"] = new SelectList(_context.SiteEmployeesEntities, "Id", "Name", siteEmployeesAssignEntity.SiteEmployeeId);
            return View(siteEmployeesAssignEntity);
        }

        // GET: SiteEmployeesAssign/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEmployeesAssignEntity = await _context.SiteEmployeesAssignEntities.FindAsync(id);
            if (siteEmployeesAssignEntity == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", siteEmployeesAssignEntity.EmployeeId);
            ViewData["SiteEmployeeId"] = new SelectList(_context.SiteEmployeesEntities, "Id", "Name", siteEmployeesAssignEntity.SiteEmployeeId);
            return View(siteEmployeesAssignEntity);
        }

        // POST: SiteEmployeesAssign/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SiteEmployeeId,EmployeeId,EmployeeShiftSalary,IsActive,Id")] SiteEmployeesAssignEntity siteEmployeesAssignEntity)
        {
            if (id != siteEmployeesAssignEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteEmployeesAssignEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteEmployeesAssignEntityExists(siteEmployeesAssignEntity.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", siteEmployeesAssignEntity.EmployeeId);
            ViewData["SiteEmployeeId"] = new SelectList(_context.SiteEmployeesEntities, "Id", "Name", siteEmployeesAssignEntity.SiteEmployeeId);
            return View(siteEmployeesAssignEntity);
        }

        // GET: SiteEmployeesAssign/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEmployeesAssignEntity = await _context.SiteEmployeesAssignEntities
                .Include(s => s.Employee)
                .Include(s => s.SiteEmployee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteEmployeesAssignEntity == null)
            {
                return NotFound();
            }

            return View(siteEmployeesAssignEntity);
        }

        // POST: SiteEmployeesAssign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var siteEmployeesAssignEntity = await _context.SiteEmployeesAssignEntities.FindAsync(id);
            _context.SiteEmployeesAssignEntities.Remove(siteEmployeesAssignEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteEmployeesAssignEntityExists(long id)
        {
            return _context.SiteEmployeesAssignEntities.Any(e => e.Id == id);
        }
    }
}
