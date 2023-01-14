using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Core.Models;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class TreasuryDepositPermissionsController : Controller
    {
        private readonly AppDbContext _context;

        public TreasuryDepositPermissionsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var appDbContext = await _context.TreasuryDepositPermission.Include(t => t.Type).ToListAsync();
            ViewBag.PermissionsNumber = appDbContext.Count;

            return View( appDbContext);
        }
        public async Task<IActionResult> Create()
        {
            var TypesList = await _context.TreasuryDepositPermissionTypesLookup.ToListAsync();

            var model = new DepositPermissionModel()
            {
                TypesList = TypesList,
            };

            return View(model);
}
            [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Value,Description,Id,TypeId,BenificiaryCode")] TreasuryDepositPermissionEntity permission)
        {
            if (ModelState.IsValid)
            {
                TreasuryDepositPermissionEntity permissionEntity = new TreasuryDepositPermissionEntity()
                {
                    Date = permission.Date,
                    Value = permission.Value,
                    Description = permission.Description,
                    TypeId = permission.TypeId,
                    BenificiaryCode = permission.BenificiaryCode,
                };

                _context.Add(permissionEntity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }




        // GET: Sites/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depositEntity = await _context.TreasuryDepositPermission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depositEntity == null)
            {
                return NotFound();
            }

            TreasuryDepositPermissionEntity site = new TreasuryDepositPermissionEntity()
            {
                Id = depositEntity.Id,
                Date = depositEntity.Date,
                Value = depositEntity.Value,
                Description = depositEntity.Description,
                TypeId = depositEntity.TypeId,

            };

            if (depositEntity == null)
            {
                return NotFound();
            }

            return View(site);
        }


        // GET: Sites/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depositEntity = await _context.TreasuryDepositPermission.FindAsync(id);
            if (depositEntity == null)
            {
                return NotFound();
            }
            List<string> list = new List<string>()
            {
                "Owner",
                "Client",
                "Bank"
            };

            ViewBag.list = new SelectList(list);
            return View(depositEntity);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Date,Value,TypeId,Id, Description")] TreasuryDepositPermissionEntity depositEntity)
        {
            if (id != depositEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depositEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionEntityExists(depositEntity.Id))
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
            List<string> list = new List<string>()
            {
                "Owner",
                "Client",
                "Bank"
            };

            ViewBag.list = new SelectList(list);
            return View(depositEntity);
        }

        // GET: Sites/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depositEntity = await _context.TreasuryDepositPermission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depositEntity == null)
            {
                return NotFound();
            }

            return View(depositEntity);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var depositEntity = await _context.TreasuryDepositPermission.FindAsync(id);
            _context.TreasuryDepositPermission.Remove(depositEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool PermissionEntityExists(long id)
        {
            return _context.TreasuryDepositPermission.Any(e => e.Id == id);
        }


        [HttpPost]
        public JsonResult PopulateBenfCode(int id)
        {

            var BankAccounts = new List<BankAccountsEntity>();
            var Customers = new List<CustomersEntity>();
            var Employees = new List<EmployeesEntity>();

            if (id == 0) return Json(new object[] { "why is it coming in with zero" });

            if (id == 4 || id == 2)
            {
                Employees = _context.EmployeesEntities.ToList();
                return Json(Employees);

            }
            else if (id == 3)
            {
                BankAccounts = _context.BankAccounts.ToList();
                return Json(BankAccounts);

            }
            else if (id == 5)
            {
                Customers = _context.CustomersEntities.ToList();
                return Json(Customers);
            }
            return Json(new object[] { null });
        }
    }
}
