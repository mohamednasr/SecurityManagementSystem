using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MNS.Repository;
using SecurityMS.Core.Models;
using SecurityMS.Core.Models.Enums;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class SiteEmployeeAttendanceController : Controller
    {
        private readonly AppDbContext _context;

        public SiteEmployeeAttendanceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SiteEmployeeAttendance
        public async Task<IActionResult> Index([FromQuery] string fromDate, [FromQuery] string toDate, [FromQuery] string employeeName, [FromQuery] string siteId, [FromQuery] string shiftTypeId)
        {
            var appDbContext = _context.SiteEmployeeAttendanceEntities
                                    .Include(s => s.AttendanceStatus).Include(s => s.Employee).Include(s => s.ShiftType).Include(s => s.Site)
                                    .WhereIf(!string.IsNullOrEmpty(fromDate) && !string.IsNullOrWhiteSpace(fromDate), x => x.AttendanceDate >= DateTime.Parse(fromDate))
                                    .WhereIf(!string.IsNullOrEmpty(toDate) && !string.IsNullOrWhiteSpace(toDate), x => x.AttendanceDate <= DateTime.Parse(toDate))
                                    .WhereIf(!string.IsNullOrEmpty(employeeName) && !string.IsNullOrWhiteSpace(employeeName), x => x.Employee.Name.Contains(employeeName))
                                    .WhereIf(!string.IsNullOrEmpty(siteId) && !string.IsNullOrWhiteSpace(siteId), x => x.SiteId == long.Parse(siteId))
                                    .WhereIf(!string.IsNullOrEmpty(shiftTypeId) && !string.IsNullOrWhiteSpace(shiftTypeId), x => x.ShiftId == long.Parse(shiftTypeId));
            var ShiftTypes = new List<ShiftTypesLookup>();
            ShiftTypes.Add(new ShiftTypesLookup() { Id = 0, Name = "أختر الفترة" });
            ShiftTypes.AddRange(await _context.ShiftTypesLookups.ToListAsync());
            ViewData["ShiftId"] = new SelectList(ShiftTypes, "Id", "Name");

            var Sites = new List<SitesEntity>();
            Sites.Add(new SitesEntity() { Id = 0, Name = "أختر الموقع" });
            Sites.AddRange(await _context.SitesEntities.ToListAsync());
            ViewData["SiteId"] = new SelectList(Sites, "Id", "Name");
            return View(await appDbContext.ToListAsync());
        }

        // GET: SiteEmployeeAttendance/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEmployeeAttendanceEntity = await _context.SiteEmployeeAttendanceEntities
                .Include(s => s.AttendanceStatus)
                .Include(s => s.Employee)
                .Include(s => s.ShiftType)
                .Include(s => s.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteEmployeeAttendanceEntity == null)
            {
                return NotFound();
            }

            return View(siteEmployeeAttendanceEntity);
        }

        // GET: SiteEmployeeAttendance/Create
        public async Task<IActionResult> Create()
        {
            var AttendanceStatuses = new List<AttendanceStatusLookup>();
            AttendanceStatuses.Add(new AttendanceStatusLookup() { Id = 0, Name = "أختر الحالة" });
            AttendanceStatuses.AddRange(await _context.Set<AttendanceStatusLookup>().ToListAsync());
            ViewData["AttendanceStatusId"] = new SelectList(AttendanceStatuses, "Id", "Name");

            var Employees = new List<EmployeesEntity>();
            Employees.Add(new EmployeesEntity() { Id = 0, Name = "أختر الموظف" });
            Employees.AddRange(await _context.EmployeesEntities.ToListAsync());
            ViewData["EmployeeId"] = new SelectList(Employees, "Id", "Name");

            var ShiftTypes = new List<ShiftTypesLookup>();
            ShiftTypes.Add(new ShiftTypesLookup() { Id = 0, Name = "أختر الفترة" });
            ShiftTypes.AddRange(await _context.ShiftTypesLookups.ToListAsync());
            ViewData["ShiftId"] = new SelectList(ShiftTypes, "Id", "Name");

            var Sites = new List<SitesEntity>();
            Sites.Add(new SitesEntity() { Id = 0, Name = "أختر الموقع" });
            Sites.AddRange(await _context.SitesEntities.ToListAsync());
            ViewData["SiteId"] = new SelectList(Sites, "Id", "Name");
            return View();
        }

        // POST: SiteEmployeeAttendance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,SiteId,AttendanceDate,ShiftId,AttendanceStatusId,Id")] SiteEmployeeAttendanceEntity siteEmployeeAttendanceEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siteEmployeeAttendanceEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AttendanceStatusId"] = new SelectList(_context.Set<AttendanceStatusLookup>(), "Id", "Id", siteEmployeeAttendanceEntity.AttendanceStatusId);
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", siteEmployeeAttendanceEntity.EmployeeId);
            ViewData["ShiftId"] = new SelectList(_context.ShiftTypesLookups, "Id", "Id", siteEmployeeAttendanceEntity.ShiftId);
            ViewData["SiteId"] = new SelectList(_context.SitesEntities, "Id", "Id", siteEmployeeAttendanceEntity.SiteId);
            return View(siteEmployeeAttendanceEntity);
        }

        // GET: SiteEmployeeAttendance/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEmployeeAttendanceEntity = await _context.SiteEmployeeAttendanceEntities.FindAsync(id);
            if (siteEmployeeAttendanceEntity == null)
            {
                return NotFound();
            }
            ViewData["AttendanceStatusId"] = new SelectList(_context.Set<AttendanceStatusLookup>(), "Id", "Id", siteEmployeeAttendanceEntity.AttendanceStatusId);
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", siteEmployeeAttendanceEntity.EmployeeId);
            ViewData["ShiftId"] = new SelectList(_context.ShiftTypesLookups, "Id", "Id", siteEmployeeAttendanceEntity.ShiftId);
            ViewData["SiteId"] = new SelectList(_context.SitesEntities, "Id", "Id", siteEmployeeAttendanceEntity.SiteId);
            return View(siteEmployeeAttendanceEntity);
        }

        // POST: SiteEmployeeAttendance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("EmployeeId,SiteId,AttendanceDate,ShiftId,AttendanceStatusId,Id")] SiteEmployeeAttendanceEntity siteEmployeeAttendanceEntity)
        {
            if (id != siteEmployeeAttendanceEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteEmployeeAttendanceEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteEmployeeAttendanceEntityExists(siteEmployeeAttendanceEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AttendanceStatusId"] = new SelectList(_context.Set<AttendanceStatusLookup>(), "Id", "Id", siteEmployeeAttendanceEntity.AttendanceStatusId);
            ViewData["EmployeeId"] = new SelectList(_context.EmployeesEntities, "Id", "Name", siteEmployeeAttendanceEntity.EmployeeId);
            ViewData["ShiftId"] = new SelectList(_context.ShiftTypesLookups, "Id", "Id", siteEmployeeAttendanceEntity.ShiftId);
            ViewData["SiteId"] = new SelectList(_context.SitesEntities, "Id", "Id", siteEmployeeAttendanceEntity.SiteId);
            return View(siteEmployeeAttendanceEntity);
        }

        // GET: SiteEmployeeAttendance/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siteEmployeeAttendanceEntity = await _context.SiteEmployeeAttendanceEntities
                .Include(s => s.AttendanceStatus)
                .Include(s => s.Employee)
                .Include(s => s.ShiftType)
                .Include(s => s.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteEmployeeAttendanceEntity == null)
            {
                return NotFound();
            }

            return View(siteEmployeeAttendanceEntity);
        }

        // POST: SiteEmployeeAttendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var siteEmployeeAttendanceEntity = await _context.SiteEmployeeAttendanceEntities.FindAsync(id);
            _context.SiteEmployeeAttendanceEntities.Remove(siteEmployeeAttendanceEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddNewEmployeeStatus()
        {
            var AttendanceStatuses = new List<AttendanceStatusLookup>();
            AttendanceStatuses.Add(new AttendanceStatusLookup() { Id = 0, Name = "أختر الحالة" });
            AttendanceStatuses.AddRange(await _context.Set<AttendanceStatusLookup>().ToListAsync());
            ViewData["AttendanceStatusId"] = new SelectList(AttendanceStatuses, "Id", "Name", ((long)AttendanceStatusEnum.Attend).ToString());

            var Employees = new List<EmployeesEntity>();
            Employees.Add(new EmployeesEntity() { Id = 0, Name = "أختر الموظف" });
            Employees.AddRange(await _context.EmployeesEntities.ToListAsync());
            ViewData["EmployeeId"] = new SelectList(Employees, "Id", "Name");

            var ShiftTypes = new List<ShiftTypesLookup>();
            ShiftTypes.Add(new ShiftTypesLookup() { Id = 0, Name = "أختر الفترة" });
            ShiftTypes.AddRange(await _context.ShiftTypesLookups.ToListAsync());
            ViewData["ShiftId"] = new SelectList(ShiftTypes, "Id", "Name");
            return PartialView("_attendanceStatus");
        }


        public async Task<IActionResult> CreateSiteAttendance(long id)
        {

            var AttendanceStatuses = new List<AttendanceStatusLookup>();
            AttendanceStatuses.Add(new AttendanceStatusLookup() { Id = 0, Name = "أختر الحالة" });
            AttendanceStatuses.AddRange(await _context.Set<AttendanceStatusLookup>().ToListAsync());
            ViewData["AttendanceStatusId"] = new SelectList(AttendanceStatuses, "Id", "Name", ((long)AttendanceStatusEnum.Attend).ToString());

            var Employees = new List<EmployeesEntity>();
            Employees.Add(new EmployeesEntity() { Id = 0, Name = "أختر الموظف" });
            Employees.AddRange(await _context.EmployeesEntities.ToListAsync());
            ViewData["EmployeeId"] = new SelectList(Employees, "Id", "Name");

            var ShiftTypes = new List<ShiftTypesLookup>();
            ShiftTypes.Add(new ShiftTypesLookup() { Id = 0, Name = "أختر الفترة" });
            ShiftTypes.AddRange(await _context.ShiftTypesLookups.ToListAsync());
            ViewData["ShiftId"] = new SelectList(ShiftTypes, "Id", "Name");

            var Sites = new List<SitesEntity>();
            Sites.Add(new SitesEntity() { Id = 0, Name = "أختر الموقع" });
            Sites.AddRange(await _context.SitesEntities.ToListAsync());
            ViewData["SiteId"] = new SelectList(Sites, "Id", "Name");


            var siteStatus = await _context.SiteEmployeesAssignEntities.Include(s => s.Employee).Include(s => s.SiteEmployee).ThenInclude(s => s.Site)
                .Where(s => s.SiteEmployee.SiteId == id).ToListAsync();

            SiteAttendanceModel siteAttendance = new SiteAttendanceModel()
            {
                SiteId = id,
                AttendanceDate = DateTime.Now,
                EmployeesStatus = siteStatus.Select(s => new AttendanceModel()
                {
                    EmployeeId = s.EmployeeId,
                    AttendanceStatusId = 1,
                    ShiftId = s.SiteEmployee.ShiftTypeId
                }).ToList()
            };

            return View("Create", siteAttendance);
        }

        [HttpPost]
        public async Task<bool> SaveAttendance([FromBody] SiteAttendanceModel attendance)
        {
            try
            {
                List<SiteEmployeeAttendanceEntity> _list = new List<SiteEmployeeAttendanceEntity>();

                attendance.EmployeesStatus.ForEach(e =>
                {
                    SiteEmployeeAttendanceEntity employeeStatus = new SiteEmployeeAttendanceEntity()
                    {
                        SiteId = attendance.SiteId,
                        AttendanceDate = attendance.AttendanceDate,
                        EmployeeId = e.EmployeeId,
                        ShiftId = e.ShiftId,
                        AttendanceStatusId = e.AttendanceStatusId,
                        Penality = e.Penality
                    };
                    _list.Add(employeeStatus);
                });
                _context.SiteEmployeeAttendanceEntities.AttachRange(_list);
                await _context.SaveChangesAsync();
                return true; //RedirectToAction("Details", "Sites", new { id = attendance.SiteId });
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool SiteEmployeeAttendanceEntityExists(long id)
        {
            return _context.SiteEmployeeAttendanceEntities.Any(e => e.Id == id);
        }
    }
}
