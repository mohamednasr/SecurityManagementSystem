using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Core.Models;
using SecurityMS.Core.Models.Enums;
using SecurityMS.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class SecurityPersonsSalaryReportsController : Controller
    {
        private readonly AppDbContext _context;

        public SecurityPersonsSalaryReportsController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(SalarySearchModel searchModel)
        {
            SecurityPersonsSalaryReportModel reportModel = new SecurityPersonsSalaryReportModel()
            {
                Data = new List<SecurityPersonsSalaryReport>(),
                searchModel = new SalarySearchModel()
            };

            var result = await  _context.SiteEmployeeAttendanceEntities.Include(x => x.Employee).Include(x => x.Site).Where(x => x.AttendanceDate.Month == searchModel.Month && x.AttendanceDate.Year == searchModel.Year).ToListAsync();

            var employees = result.GroupBy(r => r.EmployeeId);
            List<SecurityPersonsSalaryReport> report = new List<SecurityPersonsSalaryReport>();
            foreach(var emp in employees)
            {
                SecurityPersonsSalaryReport employeeStatus = new SecurityPersonsSalaryReport()
                {
                    EmployeeName = emp.FirstOrDefault().Employee.Name,
                    Attendance = emp.Where(x => x.AttendanceStatusId == (long)AttendanceStatusEnum.Attend).Count(),
                    BreakDays = emp.Where(x => x.AttendanceStatusId == (long)AttendanceStatusEnum.Break).Count(),
                    Apologizes = emp.Where(x => x.AttendanceStatusId == (long)AttendanceStatusEnum.Aplogize).Count(),
                    Absence = emp.Where(x => x.AttendanceStatusId == (long)AttendanceStatusEnum.Absence).Count(),
                    Vacation = emp.Where(x => x.AttendanceStatusId == (long)AttendanceStatusEnum.Vacation).Count(),
                };

                var siteSalary = await _context.SiteEmployeesAssignEntities.Include(s => s.SiteEmployee).Where(x => x.EmployeeId == emp.Key || x.SiteEmployeeId == emp.FirstOrDefault().SiteId).Select(x => x.SiteEmployee.EmployeeShiftSalary).FirstOrDefaultAsync();

                employeeStatus.FinalSalary = (employeeStatus.Attendance + employeeStatus.BreakDays + employeeStatus.Vacation + employeeStatus.Apologizes) * siteSalary;

                report.Add(employeeStatus);
            }

            reportModel.Data = report;
            reportModel.searchModel = searchModel;
            List<SecurityPersonsSalaryReport> salariesModel = new List<SecurityPersonsSalaryReport>();
            return View(reportModel);
        }
    }
}
