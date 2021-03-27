using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;

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
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SiteEmployeesAssignEntities.Include(s => s.Employee).Include(s => s.SiteEmployee);
            return View(await appDbContext.ToListAsync());
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
                return RedirectToAction(nameof(Index));
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
