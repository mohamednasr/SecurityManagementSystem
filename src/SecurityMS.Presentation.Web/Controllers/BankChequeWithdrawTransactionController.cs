using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class BankChequeWithdrawTransactionController : Controller
    {
        private readonly AppDbContext _context;

        public BankChequeWithdrawTransactionController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var appDbContext = await _context.BankChequeWithdrawTransaction.Include(e => e.BankAccount).ToListAsync();
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
        public async Task<IActionResult> Create([Bind("Date,Value,Description,Id,Direction,BankId,TransactionNumber")] BankChequeWithdrawTransaction transaction)
        {
            if (ModelState.IsValid)
            {

                BankChequeWithdrawTransaction transactionEntity = new BankChequeWithdrawTransaction()
                {
                    Date = transaction.Date,
                    Value = transaction.Value,
                    Description = transaction.Description,
                    BankId = transaction.BankId,
                    Direction = transaction.Direction,
                    TransactionNumber = transaction.TransactionNumber

                };
                BankAccountsEntity Bank = _context.BankAccounts.FindAsync(transaction.BankId).Result;
                Bank.CurrentBalance -= transaction.Value;

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

            var transaction = await _context.BankChequeWithdrawTransaction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            BankChequeWithdrawTransaction bankTransaction = new BankChequeWithdrawTransaction()
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

            var transaction = await _context.BankChequeWithdrawTransaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Date,Value,Description,Id,Direction,BankId,TransactionNumber")] BankChequeWithdrawTransaction transaction)
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

            return View(transaction);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.BankChequeWithdrawTransaction
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



            var transaction = await _context.BankChequeWithdrawTransaction.FindAsync(id);

            BankAccountsEntity Bank = _context.BankAccounts.FindAsync(transaction.BankId).Result;

            Bank.CurrentBalance += transaction.Value;


            _context.BankChequeWithdrawTransaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = transaction.BankId });
        }


        private bool TransactionEntityExists(long id)
        {
            return _context.BankChequeWithdrawTransaction.Any(e => e.Id == id);
        }

    }
}
