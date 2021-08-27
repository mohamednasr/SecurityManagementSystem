using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Core.Models;
using SecurityMS.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class MonthRevenuePerCompanyReportController : Controller
    {
        private readonly AppDbContext _context;

        public MonthRevenuePerCompanyReportController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(MonthRevenuReportSearchModel searchModel)
        {
            MonthRevenuReportModel reportModel = new MonthRevenuReportModel()
            {
                Data = new List<MonthRevenuReport>(),
                searchModel = new MonthRevenuReportSearchModel()
            };

            var result = await  _context.SiteEmployeeAttendanceEntities.Include(x => x.Employee).Include(x => x.Site).ThenInclude(x => x.Contracts).ThenInclude(x => x.MainCustomer).Where(x => x.AttendanceDate.Month == searchModel.Month && x.AttendanceDate.Year == searchModel.Year).ToListAsync();

            var companies = result.GroupBy(r => r.Site.Contracts.CustomerId);
            List<MonthRevenuReport> report = new List<MonthRevenuReport>();
            foreach(var company in companies)
            {
                MonthRevenuReport employeeStatus = new MonthRevenuReport()
                {
                    CompanyName = company.FirstOrDefault().Site.Contracts.MainCustomer.Name,
                    SitesCount = company.Count()
                };

                var ContractIncome = _context.SiteEmployeesEntities.Where(x => x.Site.Contracts.CustomerId == company.Key).Sum(x => x.EmployeesPerShift * x.ShiftValue);

                employeeStatus.FinalIncome = ContractIncome;

                report.Add(employeeStatus);
            }

            reportModel.Data = report;
            reportModel.searchModel = searchModel;
            List<SecurityPersonsSalaryReport> salariesModel = new List<SecurityPersonsSalaryReport>();
            return View(reportModel);
        }
    }
}
