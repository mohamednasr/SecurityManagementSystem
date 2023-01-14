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
    public class BankCashDepositTransactionController : Controller
    {
        private readonly AppDbContext _context;

        public BankCashDepositTransactionController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var appDbContext = await _context.BankCashDepositTransaction.Include(e=>e.BankAccount).ToListAsync();

            ViewBag.Transactions = appDbContext.Count;
            return View(appDbContext);
        }
        public IActionResult Create(int? id)
        {
            ViewBag.BankId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Value,Description,Id,Direction,BankId,TransactionNumber")] BankCashDepositTransaction transaction)
        {
            if (ModelState.IsValid)
            {

                BankCashDepositTransaction transactionEntity = new BankCashDepositTransaction()
                {
                    Date = transaction.Date,
                    Value = transaction.Value,
                    Description = transaction.Description,
                    BankId = transaction.BankId,
                    Direction = transaction.Direction,
                    TransactionNumber = transaction.TransactionNumber

                };
                BankAccountsEntity Bank = _context.BankAccounts.FindAsync(transaction.BankId).Result;

                Bank.CurrentBalance += transaction.Value;

                _context.Update(Bank);

                _context.Add(transactionEntity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { id = transaction.BankId });
            }
            return View();
        }




        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.BankCashDepositTransaction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            BankCashDepositTransaction bankTransaction = new BankCashDepositTransaction()
            {
                Date = transaction.Date,
                Value = transaction.Value,
                Description = transaction.Description,
                BankId = transaction.BankId,
                Direction = transaction.Direction,
                TransactionNumber = transaction.TransactionNumber
            };

            if (bankTransaction == null)
            {
                return NotFound();
            }

            return View(bankTransaction);
        }


        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.BankCashDepositTransaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Date,Value,Description,Id,Direction,BankId,TransactionNumber")] BankCashDepositTransaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionEntityExists(transaction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = transaction.BankId });

            }
            List<string> list = new List<string>()
            {
                "ايداع نقدي",
                "ايداع شيك",
                "سحب شيك",
                "سحب نقدي "
            };

            ViewBag.list = new SelectList(list);
            return View(transaction);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.BankCashDepositTransaction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {



            var transaction = await _context.BankCashDepositTransaction.FindAsync(id);

            BankAccountsEntity Bank = _context.BankAccounts.FindAsync(transaction.BankId).Result;

            Bank.CurrentBalance -= transaction.Value;

            _context.BankCashDepositTransaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = transaction.BankId });
        }


        private bool TransactionEntityExists(long id)
        {
            return _context.BankCashDepositTransaction.Any(e => e.Id == id);
        }

    }
}
