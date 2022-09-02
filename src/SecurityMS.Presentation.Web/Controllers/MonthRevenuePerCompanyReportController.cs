using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Core.Models;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
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

            var result = await _context.SiteEmployeeAttendanceEntities.Include(x => x.Employee).Include(x => x.Site).ThenInclude(x => x.Contracts).ThenInclude(x => x.MainCustomer)
                .Where(x => x.AttendanceDate.Month == searchModel.Month && x.AttendanceDate.Year == searchModel.Year).ToListAsync();

            var companies = result.GroupBy(r => r.Site.Contracts.CustomerId);
            List<MonthRevenuReport> report = new List<MonthRevenuReport>();
            foreach (var company in companies)
            {
                MonthRevenuReport employeeStatus = new MonthRevenuReport()
                {
                    CompanyId = company.FirstOrDefault().Site.Contracts.MainCustomer.Id,
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

        [HttpGet]
        [Route("createInvoice/{id}/{month}/{year}")]
        public async Task<IActionResult> CreateInvoice(long id, int month, int year)
        {
            var result = await _context.SiteEmployeeAttendanceEntities.Include(x => x.Employee).Include(x => x.Site).ThenInclude(x => x.Contracts).ThenInclude(x => x.MainCustomer)
                .Where(x => x.AttendanceDate.Month == month && x.AttendanceDate.Year == year).ToListAsync();
            var ContractIncome = _context.SiteEmployeesEntities.Include(x => x.Job).Include(x => x.Site).ThenInclude(x => x.Contracts).ThenInclude(x => x.MainCustomer).ThenInclude(x => x.ParentCustomers).Where(x => x.Site.Contracts.CustomerId == id).ToList();

            var items = ContractIncome.Select(x => new InvoiceDetails()
            {
                Count = x.EmployeesPerShift,
                ItemName = x.Job.Name,
                Price = x.ShiftValue,
                Total = x.EmployeesPerShift * x.ShiftValue
            }).ToList();
            InvoiceEntity invoice = new InvoiceEntity()
            {
                CompanyId = id,
                CompanyName = ContractIncome.FirstOrDefault().Site.Contracts.MainCustomer.ParentCustomers.Name,
                InvoiceDate = DateTime.Now,
                FinalIncome = ContractIncome.Sum(x => x.EmployeesPerShift * x.ShiftValue),
                items = items
            };

            await _context.InvoicesEntity.AddAsync(invoice);
            await _context.SaveChangesAsync();

            return RedirectToAction("CreateCustomerInvoice", "Invoices", new { invoiceId = invoice.Id });
        }
    }
}
