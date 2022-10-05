using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly AppDbContext _context;

        public PurchasesController(AppDbContext context)
        {
            _context = context;
        }
        // GET: PurchasesController
        public async Task<IActionResult> Index()
        {

            return View(await _context.Purchases.ToListAsync());
        }

        // GET: PurchasesController/Create
        public async Task<IActionResult> Create()
        {
            var suppliers = new List<Supplier>();
            suppliers.Add(new Supplier() { SupplierName = "أختر المورد" });
            suppliers.AddRange(await _context.Suppliers.ToListAsync());
            ViewData["Suppliers"] = new SelectList(suppliers, "Id", "SupplierName");
            return View();
        }

        // POST: PurchasesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PurchaseDate,SupplierId,SupplyTypeId,ItemId,Quantity,Id")] Purchases PurchaseRequest)
        {
            if (ModelState.IsValid)
            {
                PurchaseRequest.create(HttpContext.User.Identity.Name);
                _context.Add(PurchaseRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var suppliers = new List<Supplier>();
            suppliers.Add(new Supplier() { SupplierName = "أختر المورد" });
            suppliers.AddRange(await _context.Suppliers.ToListAsync());
            ViewData["SupplyTypes"] = new SelectList(suppliers, "Id", "SupplierName");

            var supplyTypes = new List<SupplyTypes>();
            supplyTypes.Add(new SupplyTypes() { SupplyName = "أختر نوع الصنف" });
            supplyTypes.AddRange(await _context.SupplyTypes.ToListAsync());
            ViewData["SupplyTypes"] = new SelectList(supplyTypes, "Id", "SupplyName");

            return View(PurchaseRequest);
        }

        // GET: PurchasesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PurchasesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PurchasesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PurchasesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
