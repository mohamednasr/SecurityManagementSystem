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
    public class TreasuryWithdrawPermissionsController : Controller
    {
        private readonly AppDbContext _context;

        public TreasuryWithdrawPermissionsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var appDbContext = await _context.TreasuryWithdrawPermission.ToListAsync();
            ViewBag.PermissionsNumber = appDbContext.Count;
            return View(appDbContext);

        }
        public IActionResult Create()
        {
            List<string> list = new List<string>()
            {
                "Supplier",
                "Assets Buying",
                "Treasury Payment"
            };

            ViewBag.list = new SelectList(list);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Value,Description,Id,TypeId")] TreasuryWithdrawPermissionEntity permission)
        {
            if (ModelState.IsValid)
            {
                TreasuryWithdrawPermissionEntity permissionEntity = new TreasuryWithdrawPermissionEntity()
                {
                    Date = permission.Date,
                    Value = permission.Value,
                    Description = permission.Description,
                    TypeId = permission.TypeId
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
                TypeId = withdrawentity.TypeId,

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
            if (withdrawentity == null)
            {
                return NotFound();
            }
            List<string> list = new List<string>()
            {
                "Supplier",
                "Assets Buying",
                "Treasury Payment"
            };

            ViewBag.list = new SelectList(list);
            return View(withdrawentity);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Date,Value,TypeId,Id, Description")] TreasuryWithdrawPermissionEntity withdrawentity)
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
            List<string> list = new List<string>()
            {
                "Supplier",
                "Assets Buying",
                "Treasury Payment"
            };


            ViewBag.list = new SelectList(list);
            return View(withdrawentity);
        }

        // GET: Sites/Delete/5
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

        // POST: Sites/Delete/5
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
    }
}
