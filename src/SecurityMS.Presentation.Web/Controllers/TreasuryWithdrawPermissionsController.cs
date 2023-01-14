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
    public class TreasuryWithdrawPermissionsController : Controller
    {
        private readonly AppDbContext _context;

        public TreasuryWithdrawPermissionsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var appDbContext = await _context.TreasuryWithdrawPermission.Include(t=>t.Type).ToListAsync();
            ViewBag.PermissionsNumber = appDbContext.Count;
            return View(appDbContext);

        }
        public async Task<IActionResult> Create()
        {
            var TypesList = await _context.TreasuryWithdrawPermissionTypesLookup.ToListAsync();

            var model = new WithdrawPermissionsModel()
            {
                TypesList = TypesList,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Value,Description,Id,TypeId,BenificiaryCode")] TreasuryWithdrawPermissionEntity permission)
        {
            if (ModelState.IsValid)
            {
                TreasuryWithdrawPermissionEntity permissionEntity = new TreasuryWithdrawPermissionEntity()
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

            var withdrawentity = await _context.TreasuryWithdrawPermission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (withdrawentity == null)
            {
                return NotFound();
            }

            TreasuryWithdrawPermissionEntity site = new TreasuryWithdrawPermissionEntity()
            {
                Id = withdrawentity.Id,
                Date = withdrawentity.Date,
                Value = withdrawentity.Value,
                Description = withdrawentity.Description,
                Type = withdrawentity.Type,

            };

            if (withdrawentity == null)
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

            var withdrawentity = await _context.TreasuryWithdrawPermission.FindAsync(id);
            var TypesList = await _context.TreasuryWithdrawPermissionTypesLookup.ToListAsync();

            var model = new WithdrawPermissionsModel()
            {
                permission = withdrawentity,
                TypesList = TypesList,
            };

            if (withdrawentity == null)
            {
                return NotFound();
            }


            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Date,Value,Description,TypeId,BenificiaryCode,Id")] TreasuryWithdrawPermissionEntity withdrawentity)
        {
            if (id != withdrawentity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(withdrawentity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionEntityExists(withdrawentity.Id))
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
            var TypesList = await _context.TreasuryWithdrawPermissionTypesLookup.ToListAsync();

            var model = new WithdrawPermissionsModel()
            {
                permission = withdrawentity,
                TypesList = TypesList,
            };

            return View(withdrawentity);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var withdrawentity = await _context.TreasuryWithdrawPermission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (withdrawentity == null)
            {
                return NotFound();
            }

            return View(withdrawentity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var withdrawentity = await _context.TreasuryWithdrawPermission.FindAsync(id);
            _context.TreasuryWithdrawPermission.Remove(withdrawentity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool PermissionEntityExists(long id)
        {
            return _context.TreasuryWithdrawPermission.Any(e => e.Id == id);
        }

        [HttpPost]
        public  JsonResult PopulateBenfCode(int id)
        {

            var SuppliersList = new List<Supplier>();
            var ExpensesList = new List<ExpensesLookup>();
            var AssetsList = new List<AssetsLookup>();
            var Employees = new List<EmployeesEntity>();

            if(id == 0) return Json(new object[] { "why is it coming in with zero" });

            if (id == 1 || id == 2)
            {
                ExpensesList = _context.ExpensesLookup.ToList();
                return Json(ExpensesList);

            }
            else if (id == 3)
            {
                SuppliersList = _context.Suppliers.ToList();
                return Json(SuppliersList);

            }
            else if (id == 4) 
            {
                AssetsList = _context.AssetsLookup.ToList();
                return Json(AssetsList);

            }
            else if (id == 5 || id == 6)
            {
                Employees = _context.EmployeesEntities.ToList();
                return Json(Employees);

            }

            return Json(new object[] { null });
        }
    }
}
