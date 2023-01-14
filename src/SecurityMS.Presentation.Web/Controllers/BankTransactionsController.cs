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
    public class BankTransactionsController : Controller
    {

        private readonly AppDbContext _context;

        public BankTransactionsController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var appDbContext = await _context.BankTransactions.Where(T => T.BankId == id).ToListAsync();
            BankAccountsEntity Bank = _context.BankAccounts.FindAsync(id).Result;
            ViewBag.Bank = Bank;
            ViewBag.Transactions = appDbContext.Count;
            return View(appDbContext);
        }
        public IActionResult Create(int? id)
        {

            List<string> list = new List<string>()
            {
                "ايداع نقدي",
                "ايداع شيك",
                "سحب شيك",
                "سحب نقدي"
            };
            ViewBag.list = new SelectList(list);
            ViewBag.BankId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,Value,Description,Id,Type,Direction,BankId,TransactionNumber")] BankTransactions transaction)
        {
            if (ModelState.IsValid)
            {

                BankTransactions transactionEntity = new BankTransactions()
                {
                    Date = transaction.Date,
                    Value = transaction.Value,
                    Description = transaction.Description,
                    Type = transaction.Type,
                    BankId = transaction.BankId,
                    Direction = transaction.Direction,
                    TransactionNumber = transaction.TransactionNumber

                };
                BankAccountsEntity Bank = _context.BankAccounts.FindAsync(transaction.BankId).Result;

                if (transaction.Type == "ايداع نقدي" || transaction.Type == "ايداع شيك") Bank.CurrentBalance += transaction.Value;
                if (transaction.Type == "سحب شيك" || transaction.Type == "سحب نقدي") Bank.CurrentBalance -= transaction.Value;
                _context.Update(Bank);

                _context.Add(transactionEntity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { id = transaction.BankId });
            }
            List<string> list = new List<string>()
            {
                "ايداع نقدي",
                "ايداع شيك",
                "سحب شيك",
                "سحب نقدي"
            };
            ViewBag.list = new SelectList(list);
            return View();
        }




        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.BankTransactions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            BankTransactions bankTransaction = new BankTransactions()
            {
                Date = transaction.Date,
                Value = transaction.Value,
                Description = transaction.Description,
                Type = transaction.Type,
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

            var transaction = await _context.BankTransactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Date,Value,Description,Id,Type,Direction,BankId,TransactionNumber")] BankTransactions transaction)
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

            var transaction = await _context.BankTransactions
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



            var transaction = await _context.BankTransactions.FindAsync(id);

            BankAccountsEntity Bank = _context.BankAccounts.FindAsync(transaction.BankId).Result;


            if (transaction.Type == "ايداع نقدي" || transaction.Type == "ايداع شيك") Bank.CurrentBalance -= transaction.Value;

            if (transaction.Type == "سحب شيك" || transaction.Type == "سحب نقدي") Bank.CurrentBalance += transaction.Value;

            _context.BankTransactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = transaction.BankId });
        }


        private bool TransactionEntityExists(long id)
        {
            return _context.BankTransactions.Any(e => e.Id == id);
        }


    }
}
