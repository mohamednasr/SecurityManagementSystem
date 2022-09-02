using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly AppDbContext _context;

        public InvoicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            return View(await _context.InvoicesEntity.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceEntity = await _context.InvoicesEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceEntity == null)
            {
                return NotFound();
            }

            return View(invoiceEntity);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [Route("createCustomerInvoice/{invoiceId}")]
        public async Task<IActionResult> CreateCustomerInvoice(long invoiceId)
        {
            var invoice = await _context.InvoicesEntity.Include(x => x.items).Where(x => x.Id == invoiceId).FirstOrDefaultAsync();
            return View(invoice);
        }
        public async Task<IActionResult> CreateCustomer2Invoice(long invoiceId)
        {
            var invoice = await _context.InvoicesEntity.Include(x => x.items).Where(x => x.Id == invoiceId).FirstOrDefaultAsync();
            return View("CreateCustomerInvoice", invoice);
        }
        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,CompanyName,SitesCount,FinalIncome")] InvoiceEntity invoiceEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceEntity);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceEntity = await _context.InvoicesEntity.FindAsync(id);
            if (invoiceEntity == null)
            {
                return NotFound();
            }
            return View(invoiceEntity);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CompanyId,CompanyName,SitesCount,FinalIncome")] InvoiceEntity invoiceEntity)
        {
            if (id != invoiceEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceEntityExists(invoiceEntity.Id))
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
            return View(invoiceEntity);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceEntity = await _context.InvoicesEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceEntity == null)
            {
                return NotFound();
            }

            return View(invoiceEntity);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var invoiceEntity = await _context.InvoicesEntity.FindAsync(id);
            _context.InvoicesEntity.Remove(invoiceEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceEntityExists(long id)
        {
            return _context.InvoicesEntity.Any(e => e.Id == id);
        }
    }
}
