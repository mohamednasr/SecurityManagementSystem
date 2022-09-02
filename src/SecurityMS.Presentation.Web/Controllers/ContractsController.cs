using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class ContractsController : Controller
    {
        private readonly AppDbContext _context;
        private static Uploader _uploader = new Uploader();

        public ContractsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ContractsEntities.Include(c => c.ContactPerson).Include(c => c.MainCustomer);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractsEntity = await _context.ContractsEntities
                .Include(c => c.ContactPerson)
                .Include(c => c.MainCustomer)
                .Include(c => c.ContractSites)
                .ThenInclude(x => x.zone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contractsEntity == null)
            {
                return NotFound();
            }

            return View(contractsEntity);
        }

        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["ContractContactPersonId"] = new SelectList(_context.CustomerContactsEntities, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(_context.CustomersEntities, "Id", "Name");
            return View();
        }

        // POST: Contracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,StartDate,EndDate,CustomerId,ContractContactPersonId,TaxPercentage,CommercialProfits,Id")] ContractsEntity contractsEntity)
        {
            if (ModelState.IsValid)
            {
                var uploads = _uploader.uploadFile(HttpContext, "\\uploads\\");
                if (uploads.Count == 1)
                    contractsEntity.ContractPDF = uploads.FirstOrDefault().Value;
                _context.Add(contractsEntity);
                await _context.SaveChangesAsync();
                //return View("Details",contractsEntity);
                return RedirectToAction(nameof(Details), "Contracts", new { id = contractsEntity.Id });
                //return RedirectToAction(nameof(Create), "Sites", contractsEntity.Id);
            }
            ViewData["ContractContactPersonId"] = new SelectList(_context.CustomerContactsEntities, "Id", "Name", contractsEntity.ContractContactPersonId);
            ViewData["CustomerId"] = new SelectList(_context.CustomersEntities, "Id", "Name", contractsEntity.CustomerId);
            return View(contractsEntity);
        }

        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractsEntity = await _context.ContractsEntities.FindAsync(id);
            if (contractsEntity == null)
            {
                return NotFound();
            }
            ViewData["ContractContactPersonId"] = new SelectList(_context.CustomerContactsEntities, "Id", "Name", contractsEntity.ContractContactPersonId);
            ViewData["CustomerId"] = new SelectList(_context.CustomersEntities, "Id", "Name", contractsEntity.CustomerId);
            return View(contractsEntity);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Date,StartDate,EndDate,CustomerId,ContractContactPersonId,TaxPercentage,CommercialProfits,Id")] ContractsEntity contractsEntity)
        {
            if (id != contractsEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var uploads = _uploader.uploadFile(HttpContext, "\\uploads\\");
                    if (uploads.Count == 1)
                        contractsEntity.ContractPDF = uploads.FirstOrDefault().Value;
                    _context.Update(contractsEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractsEntityExists(contractsEntity.Id))
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
            ViewData["ContractContactPersonId"] = new SelectList(_context.CustomerContactsEntities, "Id", "Name", contractsEntity.ContractContactPersonId);
            ViewData["CustomerId"] = new SelectList(_context.CustomersEntities, "Id", "Name", contractsEntity.CustomerId);
            return View(contractsEntity);
        }

        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractsEntity = await _context.ContractsEntities
                .Include(c => c.ContactPerson)
                .Include(c => c.MainCustomer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contractsEntity == null)
            {
                return NotFound();
            }

            return View(contractsEntity);
        }

        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var contractsEntity = await _context.ContractsEntities.FindAsync(id);
            _context.ContractsEntities.Remove(contractsEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteAttachment(long id)
        {
            if (ContractsEntityExists(id))
            {
                var contract = await _context.ContractsEntities.FirstOrDefaultAsync(x => x.Id == id);
                string deletePath = "";
                deletePath = contract.ContractPDF;
                contract.ContractPDF = null;
                _context.Update(contract);
                await _context.SaveChangesAsync();
                _uploader.DeleteFile(deletePath);
            }
            return RedirectToAction("Edit", new { id = id });
        }
        private bool ContractsEntityExists(long id)
        {
            return _context.ContractsEntities.Any(e => e.Id == id);
        }
    }
}
