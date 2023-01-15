using Microsoft.AspNetCore.Mvc;
using SecurityMS.Core.Models;
using SecurityMS.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class BankTransactionReports : Controller
    {
        private readonly AppDbContext _context;

        public BankTransactionReports(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult PopulateReport(TreasuryWithdrawReportDateModel model)
        {

            var reports = new List<TreasuryWithdrawReportModel>();


            var totalvaluesChequeDeposit = _context.BankChequeDepositTransaction.Where(c => (c.Date >= model.StartDate) && (c.Date <= model.EndDate)).Sum(c => c.Value);
            var totalValuesChequeWithdraw = _context.BankChequeWithdrawTransaction.Where(c => (c.Date >= model.StartDate) && (c.Date <= model.EndDate)).Sum(c => c.Value);
            var totalValuesCashDeposit = _context.BankCashDepositTransaction.Where(c => (c.Date >= model.StartDate) && (c.Date <= model.EndDate)).Sum(c => c.Value);
            var totalValuesCashWithdraw = _context.BankCashWithdrawTransaction.Where(c => (c.Date >= model.StartDate) && (c.Date <= model.EndDate)).Sum(c => c.Value);
            var countChequeDeposit = _context.BankChequeDepositTransaction.Where(c => (c.Date >= model.StartDate) && (c.Date <= model.EndDate)).Count();
            var countChequeWithdraw = _context.BankChequeWithdrawTransaction.Where(c => (c.Date >= model.StartDate) && (c.Date <= model.EndDate)).Count();
            var countCashDeposit = _context.BankCashDepositTransaction.Where(c => (c.Date >= model.StartDate) && (c.Date <= model.EndDate)).Count();
            var countCashWithdraw = _context.BankCashWithdrawTransaction.Where(c => (c.Date >= model.StartDate) && (c.Date <= model.EndDate)).Count();

            reports.Add(new TreasuryWithdrawReportModel()
            {
                name = "ايداع شيك",
                count = countChequeDeposit,
                total = totalvaluesChequeDeposit,
            });
            reports.Add(new TreasuryWithdrawReportModel()
            {
                name = "سحب شيك",
                count = countChequeWithdraw,
                total = totalValuesChequeWithdraw,
            });
            reports.Add(new TreasuryWithdrawReportModel()
            {
                name = "ايداع كاش",
                count = countCashDeposit,
                total = totalValuesCashDeposit,
            });
            reports.Add(new TreasuryWithdrawReportModel()
            {
                name = "سحب كاش",
                count = countCashWithdraw,
                total = totalValuesCashWithdraw,
            });



            return Json(reports);


        }
    }
}
