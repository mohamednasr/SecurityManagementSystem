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
    public class DepartmentsController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            return View(await _context.DepartmentsEntities.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentsEntity = await _context.DepartmentsEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmentsEntity == null)
            {
                return NotFound();
            }

            return View(departmentsEntity);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] DepartmentsEntity departmentsEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departmentsEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departmentsEntity);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentsEntity = await _context.DepartmentsEntities.FindAsync(id);
            if (departmentsEntity == null)
            {
                return NotFound();
            }
            return View(departmentsEntity);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Id")] DepartmentsEntity departmentsEntity)
        {
            if (id != departmentsEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departmentsEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsEntityExists(departmentsEntity.Id))
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
            return View(departmentsEntity);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departmentsEntity = await _context.DepartmentsEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departmentsEntity == null)
            {
                return NotFound();
            }

            return View(departmentsEntity);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var departmentsEntity = await _context.DepartmentsEntities.FindAsync(id);
            _context.DepartmentsEntities.Remove(departmentsEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentsEntityExists(long id)
        {
            return _context.DepartmentsEntities.Any(e => e.Id == id);
        }
    }
}
