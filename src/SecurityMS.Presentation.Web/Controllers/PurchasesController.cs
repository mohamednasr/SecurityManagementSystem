using Microsoft.AspNetCore.Http;
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

            return View(await _context.Purchases.Include(x => x.Supplier).Include(x => x.SupplyType).ToListAsync());
        }

        // GET: PurchasesController/Create
        public async Task<IActionResult> Create()
        {
            var suppliers = new List<Supplier>();
            suppliers.Add(new Supplier() { SupplierName = "أختر المورد" });
            suppliers.AddRange(await _context.Suppliers.Where(x => !x.IsDeleted).ToListAsync());
            ViewData["Suppliers"] = new SelectList(suppliers, "Id", "SupplierName");

            var supplyTypes = new List<SupplyTypes>();
            supplyTypes.Add(new SupplyTypes() { SupplyName = "أختر نوع الصنف" });
            supplyTypes.AddRange(await _context.SupplyTypes.Where(x => !x.IsDeleted).ToListAsync());
            ViewData["SupplyTypes"] = new SelectList(supplyTypes, "Id", "SupplyName");

            var Items = new List<ItemEntity>();
            Items.Add(new ItemEntity() { Code = "أختر كود الصنف" });
            Items.AddRange(await _context.Items.Where(x => !x.IsDeleted).ToListAsync());
            ViewData["Items"] = new SelectList(Items, "Id", "Code");

            Purchases purchase = new Purchases()
            {
                Items = new List<PurchaseItem>() { new PurchaseItem() }
            };

            return View(purchase);
        }

        // POST: PurchasesController/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Purchases PurchaseRequest)
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
            suppliers.AddRange(await _context.Suppliers.Where(x => !x.IsDeleted).ToListAsync());
            ViewData["Suppliers"] = new SelectList(suppliers, "Id", "SupplierName");

            var supplyTypes = new List<SupplyTypes>();
            supplyTypes.Add(new SupplyTypes() { SupplyName = "أختر نوع الصنف" });
            supplyTypes.AddRange(await _context.SupplyTypes.Where(x => !x.IsDeleted).ToListAsync());
            ViewData["SupplyTypes"] = new SelectList(supplyTypes, "Id", "SupplyName");

            var Items = new List<ItemEntity>();
            Items.Add(new ItemEntity() { Code = "أختر كود الصنف" });
            Items.AddRange(await _context.Items.Where(x => !x.IsDeleted).ToListAsync());
            ViewData["Items"] = new SelectList(Items, "Id", "Code");

            return View(PurchaseRequest);
        }

        [HttpPost]
        public async Task<bool> CreatePurchase([FromBody] Purchases PurchaseRequest)
        {
            if (ModelState.IsValid)
            {
                PurchaseRequest.create(HttpContext.User.Identity.Name);
                _context.Add(PurchaseRequest);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewPurchaseItem()
        {
            var Items = new List<ItemEntity>();
            Items.Add(new ItemEntity() { Code = "أختر كود الصنف" });
            Items.AddRange(await _context.Items.Where(x => !x.IsDeleted).ToListAsync());
            ViewData["Items"] = new SelectList(Items, "Id", "Code");

            return PartialView("_addPurchaseItem");
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
