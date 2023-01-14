using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class BankAccountsController : Controller
    {
        private readonly AppDbContext _context;

        public BankAccountsController( AppDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> BankAccountList()
        {
            var appDbContext = await _context.BankAccounts.ToListAsync();
            ViewBag.AccountsNumber = appDbContext.Count;
            ViewBag.total = appDbContext.Sum(a => a.CurrentBalance);

            return View(appDbContext);
        }

        public IActionResult CreateTransaction(int id)
        {
            ViewBag.Id = id;

            return View();
        }
        public IActionResult Create()
        {
             return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IBAN,Currency,Name,Id,OpeningBalance")] BankAccountsEntity Account)
        {
            if (ModelState.IsValid)
            {
                BankAccountsEntity BankAccount = new BankAccountsEntity()
                {
                    Name = Account.Name,
                    IBAN = Account.IBAN,
                    Currency = Account.Currency,
                    OpeningBalance = Account.OpeningBalance,
                    CurrentBalance = Account.OpeningBalance,

                };

                _context.Add(BankAccount);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var BankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (BankAccount == null)
            {
                return NotFound();
            }

            return View(BankAccount);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var BankAccount = await _context.BankAccounts.FindAsync(id);
            _context.BankAccounts.Remove(BankAccount);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        private bool BankAccountExists(long id)
        {
            return _context.BankAccounts.Any(e => e.Id == id);
        }
    }
}
