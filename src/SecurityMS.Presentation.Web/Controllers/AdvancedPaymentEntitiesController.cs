using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Core.Models;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
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
            var Sites = new List<SitesEntity>();
            Sites.Add(new SitesEntity() { Id = 0, Name = "أختر الموقع" });
            Sites.AddRange(await _context.SitesEntities.ToListAsync());
            ViewData["SiteId"] = new SelectList(Sites, "Id", "Name");

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

        [HttpPost]
        public async Task<IActionResult> CreateSiteAdvancedPayment([Bind("SiteId, Amount, PaymentDate, InstallmentDate, installments")] SiteAdvancedPaymentsModel model)
        {
            if (ModelState.IsValid)
            {
                var Employees = new List<EmployeesEntity>();
                Employees.Add(new EmployeesEntity() { Id = 0, Name = "أختر الموظف" });
                Employees.AddRange(await _context.EmployeesEntities.ToListAsync());
                ViewData["EmployeeId"] = new SelectList(Employees, "Id", "NameCode");
                List<AdvancedPaymentEntity> siteAdvancedPayments = _context.SiteEmployeesAssignEntities.Distinct().Include(e => e.Employee).Where(s => s.SiteEmployee.SiteId == model.SiteId).Select(s => new AdvancedPaymentEntity()
                {
                    EmployeeId = s.EmployeeId,
                    Employee = s.Employee,
                    Amount = model.Amount,
                    installments = model.installments,
                    InstallmentDate = model.InstallmentDate,
                    PaymentDate = model.PaymentDate,
                }).ToList();
                return View("CreateSiteAdvancedPaymentsReview", siteAdvancedPayments);
            }
            else
            {
                var redirectUrl = Url.Action("Index");
                redirectUrl = String.Concat(redirectUrl, "#advancedPaymentModal");

                return Redirect(redirectUrl);
            }
        }

        [HttpPost]
        public async Task<bool> SaveSiteAdvancedPayments([FromBody] List<AdvancedPaymentEntity> siteAdvancedPayments)
        {
            try
            {
                foreach (var p in siteAdvancedPayments)
                {
                    p.create(HttpContext.User.Identity.Name);
                    _context.Add(p);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        // POST: AdvancedPaymentEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Amount,installments,IsAcceptable,PayedAt,PayedBy,CreatedBy,CreatedAt,AcceptedBy,AcceptedAt,Id,PaymentDate,InstallmentDate")] AdvancedPaymentEntity advancedPaymentEntity)
        {
            if (ModelState.IsValid)
            {
                advancedPaymentEntity.create(HttpContext.User.Identity.Name);
                _context.Add(advancedPaymentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Employees", new { id = advancedPaymentEntity.EmployeeId });
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
