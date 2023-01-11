using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Core.Models;
using SecurityMS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class TreasuryWithdrawReportsController : Controller
    {
        private readonly AppDbContext _context;

        public TreasuryWithdrawReportsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Details(int id)
        {
            var appDbContext = await _context.TreasuryWithdrawPermission.Where(c => c.TypeId == id).Include(t => t.Type).ToListAsync();
            return View(appDbContext);

        }



        [HttpPost]
        public JsonResult PopulateReport(TreasuryWithdrawReportDateModel model)
        {

            var types = _context.TreasuryWithdrawPermissionTypesLookup.ToList();
            var reports = new List<TreasuryWithdrawReportModel>();

            foreach (var type in types)
            {

                var totalvalues = _context.TreasuryWithdrawPermission.Where(c => (c.Date >= model.StartDate) && (c.Date <= model.EndDate) && (c.TypeId == type.Id)).Sum(c => c.Value);
                var count = _context.TreasuryWithdrawPermission.Where(c => (c.Date >= model.StartDate) && (c.Date <= model.EndDate) && (c.TypeId == type.Id)).Count();

                reports.Add(new TreasuryWithdrawReportModel()
                {
                    name = type.Name,
                    id = type.Id,
                    count = count,
                    total = totalvalues,
                });


            }
                return Json(reports);


        }
    }
}