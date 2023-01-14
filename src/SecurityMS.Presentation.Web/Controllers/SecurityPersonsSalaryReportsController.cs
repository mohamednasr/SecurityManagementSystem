using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Core.Models;
using SecurityMS.Core.Models.Enums;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using SecurityMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalaryReportDetails = SecurityMS.Infrastructure.Data.Entities.SalaryReportDetails;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class SecurityPersonsSalaryReportsController : Controller
    {
        private readonly AppDbContext _context;

        public SecurityPersonsSalaryReportsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateSalaryReport(SalarySearchModel searchModel)
        {
            searchModel = new SalarySearchModel()
            {
                SalaryFrom = searchModel.SalaryFrom.GetValueOrDefault(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)),
                SalaryTo = searchModel.SalaryTo.GetValueOrDefault(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
            };
            var Sites = new List<SitesEntity>();
            Sites.Add(new SitesEntity() { Id = 0, Name = "أختر الموقع" });
            Sites.AddRange(await _context.SitesEntities.ToListAsync());
            ViewData["SitesIds"] = new SelectList(Sites, "Id", "Name", searchModel.SiteId);

            return View(searchModel);
        }

        public async Task<IActionResult> SalaryReportData(long id)
        {
            var ExistedReport = await _context.SalariesReportDetails.Include(x => x.EmployeesSalaries).ThenInclude(e => e.Employee)
                    .Include(e => e.Site)
                    .Where(r => r.Id == id)
                    .FirstOrDefaultAsync();
            if (ExistedReport != null)
            {
                return View(ExistedReport);
            }
            return NotFound();
        }

        public async Task<IActionResult> ReviewSalaries(SalarySearchModel searchModel)
        {
            if (validateInputs(searchModel))
            {
                var Employees = new List<EmployeesEntity>();
                Employees.Add(new EmployeesEntity() { Id = 0, Name = "أختر الموظف" });
                Employees.AddRange(await _context.SiteEmployeesAssignEntities.Include(s => s.Employee).Where(s => s.SiteEmployee.SiteId == searchModel.SiteId).Select(s => s.Employee).ToListAsync());
                ViewData["EmployeeId"] = new SelectList(Employees, "Id", "NameCode");


                var ExistedReport = await _context.SalariesReportDetails.Include(x => x.EmployeesSalaries).ThenInclude(e => e.Employee)
                    .Include(e => e.Site)
                    .Where(r => r.SiteId == searchModel.SiteId && r.SalaryDateFrom == searchModel.SalaryFrom.Value && r.SalaryDateTo == searchModel.SalaryTo.Value && !r.IsDeleted)
                    .FirstOrDefaultAsync();
                if (ExistedReport != null)
                {
                    return View(ExistedReport);
                }

                var TaxesMatrix = await _context.IncomeTaxesMatrix.ToListAsync();

                var siteJobs = await _context.SiteEmployeesEntities
                    .Include(s => s.AssignedEmployees).ThenInclude(x => x.Employee).ThenInclude(e => e.Rewards)
                    .Include(s => s.AssignedEmployees).ThenInclude(x => x.Employee).ThenInclude(e => e.Penalities)
                    .Include(s => s.AssignedEmployees).ThenInclude(x => x.Employee).ThenInclude(e => e.AdvancedPayments)
                                .Where(s => s.SiteId == searchModel.SiteId && !s.IsDeleted).ToListAsync();

                var siteAttendance = await _context.SiteEmployeeAttendanceEntities
                    .Where(a => a.AttendanceDate >= searchModel.SalaryFrom.Value && a.AttendanceDate <= searchModel.SalaryTo.Value)
                    .Where(a => a.SiteId == searchModel.SiteId.Value && !a.IsDeleted)
                    .ToListAsync();

                SalaryReportDetails salaryReport = new SalaryReportDetails()
                {
                    SalaryDate = DateTime.Now,
                    SalaryDateFrom = searchModel.SalaryFrom.Value,
                    SalaryDateTo = searchModel.SalaryTo.Value,
                    SiteId = searchModel.SiteId.Value,
                    EmployeesSalaries = new List<EmployeesSalaryReportDetails>()
                };

                foreach (var sitemEmployee in siteJobs)
                {
                    sitemEmployee.AssignedEmployees.ForEach(async e =>
                    {
                        var attendanceSheet = siteAttendance.Where(s => s.EmployeeId == e.EmployeeId).ToList();
                        var absence = attendanceSheet.Where(a => a.AttendanceStatusId == (long)AttendanceStatusEnum.Absence && !a.IsDeleted).Sum(x => x.Penality.GetValueOrDefault(1));
                        var breakDays = attendanceSheet.Count(a => a.AttendanceStatusId == (long)AttendanceStatusEnum.Break && !a.IsDeleted);

                        var advancedPayments = e.Employee.AdvancedPayments.Where(x => !x.IsPayed && x.InstallmentDate <= searchModel.SalaryFrom && !x.IsDeleted).ToList();


                        var employee = new EmployeesSalaryReportDetails()
                        {
                            Id = Guid.NewGuid(),
                            EmployeeId = e.EmployeeId,
                            Employee = e.Employee,
                            BaseSalary = e.EmployeeSalary,
                            MonthSalary = (e.EmployeeSalary / 30) * ((30 - absence) + (4 - breakDays)),
                            Rewards = e.Employee.Rewards.Where(x => x.RewardDate >= salaryReport.SalaryDateFrom && x.RewardDate <= salaryReport.SalaryDateTo && !x.IsDeleted).Sum(x => x.RewardValue).GetValueOrDefault(0),
                            Penalities = e.Employee.Penalities.Where(x => x.PenalityDate >= salaryReport.SalaryDateFrom && x.PenalityDate <= salaryReport.SalaryDateTo && !x.IsDeleted).Sum(x => x.PenaltyValue).GetValueOrDefault(0),
                            AdvancePaymentInstallment = advancedPayments.Select(s => (decimal)(s.Amount / s.installments)).Sum()
                        };
                        employee.GetInsurance();
                        employee.CalculateTaxes(TaxesMatrix);
                        employee.GetTotal();
                        salaryReport.EmployeesSalaries.Add(employee);

                        advancedPayments.ForEach(a =>
                        {
                            if (a.InstallmentDate.AddMonths(a.installments) >= salaryReport.SalaryDateTo)
                            {
                                a.IsPayed = true;
                                _context.AdvancedPaymentsEntity.Update(a);
                            }
                        });

                    });
                }

                _context.SalariesReportDetails.AddRange(salaryReport);
                await _context.SaveChangesAsync();
                return View(salaryReport);
            }
            var Sites = new List<SitesEntity>();
            Sites.Add(new SitesEntity() { Id = 0, Name = "أختر الموقع" });
            Sites.AddRange(await _context.SitesEntities.ToListAsync());
            ViewData["SitesIds"] = new SelectList(Sites, "Id", "Name", searchModel.SiteId);

            return View(nameof(CreateSalaryReport), searchModel);
        }


        [HttpPost]
        public async Task<IActionResult> RecalculateSalaries([Bind("Id, EmployeeId, SalaryReportId, BaseSalary,MonthSalary, Insurance, Penalities, Rewards, AdvancePaymentInstallment, Taxes, ExtraDeductions, TotalSalary")] EmployeesSalaryReportDetails salaryReport)
        {
            var TaxesMatrix = await _context.IncomeTaxesMatrix.ToListAsync();

            var employeeSalary = await _context.SalariesReportEmployeesReports.Include(x => x.SalaryReport).Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == salaryReport.Id);
            employeeSalary.ExtraDeductions = salaryReport.ExtraDeductions;
            employeeSalary.GetInsurance();
            employeeSalary.CalculateTaxes(TaxesMatrix);
            employeeSalary.GetTotal();

            _context.SalariesReportEmployeesReports.Update(employeeSalary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ReviewSalaries), new { SiteId = employeeSalary.SalaryReport.SiteId, SalaryFrom = employeeSalary.SalaryReport.SalaryDateFrom, SalaryTo = employeeSalary.SalaryReport.SalaryDateTo });
        }
        private bool validateInputs(SalarySearchModel searchModel)
        {
            return searchModel.SiteId > 0 && searchModel.SalaryFrom.HasValue && searchModel.SalaryTo.HasValue
                && searchModel.SalaryFrom.Value < searchModel.SalaryTo.Value;

        }

        public async Task<IActionResult> Index(SalarySearchModel searchModel)
        {
            var ExistedReports = await _context.SalariesReportDetails.Include(x => x.EmployeesSalaries).ThenInclude(e => e.Employee)
                    .Include(e => e.Site)
                    .WhereIf(searchModel.SalaryFrom.HasValue, r => r.SalaryDateFrom == searchModel.SalaryFrom.Value)
                    .WhereIf(searchModel.SalaryTo.HasValue, r => r.SalaryDateTo == searchModel.SalaryTo.Value)
                    .WhereIf(searchModel.SiteId.HasValue, r => r.SiteId == searchModel.SiteId.Value)
                    .ToListAsync();
            if (ExistedReports != null)
            {
                return View(ExistedReports);
            }
            return NotFound();
        }
    }
}
