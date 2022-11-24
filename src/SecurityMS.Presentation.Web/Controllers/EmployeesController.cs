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
using Utilities;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;
        private static Uploader _uploader = new Uploader();
        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        //[HttpGet("name={name}&{employeecode}/{natid}/{job}")]
        public async Task<IActionResult> Index([FromQuery] string name, [FromQuery] string employeecode, [FromQuery] string natid, [FromQuery] string job)
        {
            var appDbContext = _context.EmployeesEntities.Include(e => e.Job)
            .WhereIf(!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name), x => x.Name.Contains(name))
            .WhereIf(!string.IsNullOrEmpty(employeecode) && !string.IsNullOrWhiteSpace(employeecode), x => x.EmployeeCode == employeecode)
            .WhereIf(!string.IsNullOrEmpty(natid) && !string.IsNullOrWhiteSpace(natid), x => x.NationalId == natid)
            .WhereIf(!string.IsNullOrEmpty(job) && !string.IsNullOrWhiteSpace(job), x => x.Job.Name.Contains(job));

            return View(await appDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesEntity = await _context.EmployeesEntities
                .Include(e => e.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeesEntity == null)
            {
                return NotFound();
            }
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel = employeeModel.convertToModel(employeesEntity);
            var site = await _context.SiteEmployeesAssignEntities.Include(x => x.SiteEmployee).ThenInclude(x => x.Site).Where(s => s.EmployeeId == employeesEntity.Id).FirstOrDefaultAsync();
            if (site != null)
            {
                employeeModel.SiteId = site.SiteEmployee.SiteId;
                //employeeModel.ShiftSalary = site.EmployeeShiftSalary;
                employeeModel.Site = site.SiteEmployee.Site;
            }
            var EndServiceReasons = new List<EndServiceReasonLookup>();
            EndServiceReasons.Add(new EndServiceReasonLookup() { Id = 0, Name = "أختر السبب" });
            EndServiceReasons.AddRange(await _context.EndServiceReasonLookup.ToListAsync());
            ViewData["Reasons"] = new SelectList(EndServiceReasons, "Id", "Name");
            return View(employeeModel);
        }

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            var jobs = new List<JobsEntity>();
            jobs.Add(new JobsEntity() { Id = 0, Name = "أختر الوظيفة" });
            jobs.AddRange(await _context.JobsEntities.ToListAsync());
            ViewData["JobId"] = new SelectList(jobs, "Id", "Name");
            var Governments = new List<GovernmentEntity>();
            Governments.Add(new GovernmentEntity() { Id = 0, Name = "أختر المحافظة" });
            Governments.AddRange(await _context.GovernmentEntities.ToListAsync());
            ViewData["Governments"] = new SelectList(Governments, "Id", "Name");
            var Zones = new List<ZonesEntity>();
            Zones.Add(new ZonesEntity() { Id = 0, Name = "أختر المنطقة" });
            Zones.AddRange(await _context.ZonesEntities.ToListAsync());
            ViewData["Zones"] = new SelectList(Zones, "Id", "Name");
            var Sites = new List<SitesEntity>();
            Sites.Add(new SitesEntity() { Id = 0, Name = "أختر الموقع" });
            Sites.AddRange(await _context.SitesEntities.ToListAsync());
            ViewData["Sites"] = new SelectList(Sites, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,EmployeeCode,NationalId,Phone,Phone2,StartDate,EndDate,InsuranceNumber,JobId,Id, FileNumber, IsIncludeBirthCertificate, IsIncludeMilitaryCertificate, IsIncludeEducationCertificate, IsIncludeIDCopy, IsIncludePersonalPhotos, IsIncludeWorkStub, IsIncludeCriminalCertificate, SiteId, ShiftSalary, InsuranceAmount, InsurancePercentage, InsuranceStartDate, InsuranceEndDate")] EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                var exist = await _context.EmployeesEntities.Where(x => x.Name == employeeModel.Name || x.EmployeeCode == employeeModel.EmployeeCode || x.NationalId == employeeModel.NationalId).FirstOrDefaultAsync();
                if (exist == null)
                {
                    var uploads = _uploader.uploadFile(HttpContext, "\\uploads\\");
                    employeeModel.addAttachmentsPath(uploads);
                    var emplpyeeEntity = employeeModel.convertToEntity();
                    emplpyeeEntity.IsActive = true;
                    await _context.AddAsync(emplpyeeEntity);
                    await _context.SaveChangesAsync();

                    //var siteEmployee = await _context.SiteEmployeesEntities.Where(s => s.JobId == emplpyeeEntity.JobId && s.SiteId == employeeModel.SiteId).FirstOrDefaultAsync();
                    //SiteEmployeesAssignEntity employeeSite = new SiteEmployeesAssignEntity()
                    //{
                    //    EmployeeId = emplpyeeEntity.Id,
                    //    IsActive = true,
                    //    SiteEmployeeId = siteEmployee.Id,
                    //    //EmployeeShiftSalary = employeeModel.ShiftSalary
                    //};
                    //await _context.AddAsync(employeeSite);
                    //await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                ViewData["employeeExist"] = "المستخدم موجود بالفعل";
            }
            var jobs = new List<JobsEntity>();
            jobs.Add(new JobsEntity() { Id = 0, Name = "أختر الوظيفة" });
            jobs.AddRange(await _context.JobsEntities.ToListAsync());
            ViewData["JobId"] = new SelectList(jobs, "Id", "Name", employeeModel.JobId);
            var Governments = new List<GovernmentEntity>();
            Governments.Add(new GovernmentEntity() { Id = 0, Name = "أختر المحافظة" });
            Governments.AddRange(await _context.GovernmentEntities.ToListAsync());
            ViewData["Governments"] = new SelectList(Governments, "Id", "Name");
            var Zones = new List<ZonesEntity>();
            Zones.Add(new ZonesEntity() { Id = 0, Name = "أختر المنطقة" });
            Zones.AddRange(await _context.ZonesEntities.ToListAsync());
            ViewData["Zones"] = new SelectList(Zones, "Id", "Name");
            var Sites = new List<SitesEntity>();
            Sites.Add(new SitesEntity() { Id = 0, Name = "أختر الموقع" });
            Sites.AddRange(await _context.SitesEntities.ToListAsync());
            ViewData["Sites"] = new SelectList(Sites, "Id", "Name");
            return View(employeeModel);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesEntity = await _context.EmployeesEntities.FindAsync(id);
            if (employeesEntity == null)
            {
                return NotFound();
            }
            var jobs = new List<JobsEntity>();
            jobs.Add(new JobsEntity() { Id = 0, Name = "أختر الوظيفة" });
            jobs.AddRange(await _context.JobsEntities.ToListAsync());
            ViewData["JobId"] = new SelectList(jobs, "Id", "Name", employeesEntity.JobId);
            var Governments = new List<GovernmentEntity>();
            Governments.Add(new GovernmentEntity() { Id = 0, Name = "أختر المحافظة" });
            Governments.AddRange(await _context.GovernmentEntities.ToListAsync());
            ViewData["Governments"] = new SelectList(Governments, "Id", "Name");
            var Zones = new List<ZonesEntity>();
            Zones.Add(new ZonesEntity() { Id = 0, Name = "أختر المنطقة" });
            Zones.AddRange(await _context.ZonesEntities.ToListAsync());
            ViewData["Zones"] = new SelectList(Zones, "Id", "Name");
            var Sites = new List<SitesEntity>();
            Sites.Add(new SitesEntity() { Id = 0, Name = "أختر الموقع" });
            Sites.AddRange(await _context.SitesEntities.ToListAsync());
            ViewData["Sites"] = new SelectList(Sites, "Id", "Name");

            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel = employeeModel.convertToModel(employeesEntity);
            var site = await _context.SiteEmployeesAssignEntities.Include(x => x.SiteEmployee).Where(s => s.EmployeeId == employeesEntity.Id).FirstOrDefaultAsync();
            if (site != null)
            {
                employeeModel.SiteId = site.SiteEmployee.SiteId;
                //employeeModel.ShiftSalary = site.EmployeeShiftSalary;
            }
            return View(employeeModel);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,EmployeeCode,NationalId,Phone,Phone2,StartDate,EndDate,InsuranceNumber,JobId,Id, FileNumber, IsIncludeBirthCertificate, IsIncludeMilitaryCertificate, IsIncludeEducationCertificate, IsIncludeIDCopy, IsIncludePersonalPhotos, IsIncludeWorkStub, IsIncludeCriminalCertificate, SiteId, ShiftSalary, BirthCertificateCopyPath, MilitaryCertificateCopyPath, EducationCertificateSoftCopyPath, IDSoftCopyPath, PersonalPhotoSoftCopyPath, WorkStubSoftCopyPath, CriminalCertificateSoftCopyPath, InsuranceAmount, InsurancePercentage, InsuranceStartDate, InsuranceEndDate")] EmployeeModel employeeModel)
        {
            if (id != employeeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var uploads = _uploader.uploadFile(HttpContext, "\\uploads\\");
                    employeeModel.addAttachmentsPath(uploads);
                    var emplpyeeEntity = employeeModel.convertToEntity();
                    _context.Update(emplpyeeEntity);
                    await _context.SaveChangesAsync();

                    //var siteEmployee = await _context.SiteEmployeesEntities.Where(s => s.JobId == emplpyeeEntity.JobId && s.SiteId == employeeModel.SiteId).FirstOrDefaultAsync();
                    //SiteEmployeesAssignEntity employeeSite = new SiteEmployeesAssignEntity()
                    //{
                    //    EmployeeId = emplpyeeEntity.Id,
                    //    IsActive = true,
                    //    SiteEmployeeId = siteEmployee.Id,
                    //    EmployeeShiftSalary = employeeModel.ShiftSalary
                    //};
                    //await _context.AddAsync(employeeSite);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesEntityExists(employeeModel.Id))
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
            var jobs = new List<JobsEntity>();
            jobs.Add(new JobsEntity() { Id = 0, Name = "أختر الوظيفة" });
            jobs.AddRange(await _context.JobsEntities.ToListAsync());
            ViewData["JobId"] = new SelectList(jobs, "Id", "Name", employeeModel.JobId);
            var Governments = new List<GovernmentEntity>();
            Governments.Add(new GovernmentEntity() { Id = 0, Name = "أختر المحافظة" });
            Governments.AddRange(await _context.GovernmentEntities.ToListAsync());
            ViewData["Governments"] = new SelectList(Governments, "Id", "Name");
            var Zones = new List<ZonesEntity>();
            Zones.Add(new ZonesEntity() { Id = 0, Name = "أختر المنطقة" });
            Zones.AddRange(await _context.ZonesEntities.ToListAsync());
            ViewData["Zones"] = new SelectList(Zones, "Id", "Name");
            var Sites = new List<SitesEntity>();
            Sites.Add(new SitesEntity() { Id = 0, Name = "أختر الموقع" });
            Sites.AddRange(await _context.SitesEntities.ToListAsync());
            ViewData["Sites"] = new SelectList(Sites, "Id", "Name");

            return View(employeeModel);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesEntity = await _context.EmployeesEntities
                .Include(e => e.Job)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeesEntity == null)
            {
                return NotFound();
            }

            return View(employeesEntity);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var employeesEntity = await _context.EmployeesEntities.FindAsync(id);
            _context.EmployeesEntities.Remove(employeesEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAttachment(long id, int attachmentType)
        {
            var employee = await _context.EmployeesEntities.FirstOrDefaultAsync(x => x.Id == id);
            string deletePath = "";
            switch (attachmentType)
            {
                case (int)EmployeeAttachmentTypeEnum.BirthCertificate:
                    deletePath = employee.BirthCertificateCopy;
                    employee.BirthCertificateCopy = null;
                    break;
                case (int)EmployeeAttachmentTypeEnum.CriminalCertificate:
                    deletePath = employee.CriminalCertificateSoftCopy;
                    employee.CriminalCertificateSoftCopy = null;
                    break;
                case (int)EmployeeAttachmentTypeEnum.EducationCertificate:
                    deletePath = employee.EducationCertificateSoftCopy;
                    employee.EducationCertificateSoftCopy = null;
                    break;
                case (int)EmployeeAttachmentTypeEnum.Identity:
                    deletePath = employee.IDSoftCopy;
                    employee.IDSoftCopy = null;
                    break;
                case (int)EmployeeAttachmentTypeEnum.MilitaryCertificate:
                    deletePath = employee.MilitaryCertificateCopy;
                    employee.MilitaryCertificateCopy = null;
                    break;
                case (int)EmployeeAttachmentTypeEnum.PersonalPhotos:
                    deletePath = employee.PersonalPhotoSoftCopy;
                    employee.PersonalPhotoSoftCopy = null;
                    break;
                case (int)EmployeeAttachmentTypeEnum.WorkStub:
                    deletePath = employee.WorkStubSoftCopy;
                    employee.WorkStubSoftCopy = null;
                    break;
                case (int)EmployeeAttachmentTypeEnum.Insurance:
                    deletePath = employee.InsurancePrintCopy;
                    employee.InsurancePrintCopy = null;
                    break;
            }
            _context.Update(employee);
            await _context.SaveChangesAsync();
            _uploader.DeleteFile(deletePath);
            return RedirectToAction("Edit", new { id = id });
        }

        [HttpGet]
        public async Task<List<LookupModel>> GetEmployeesAsLookup(string? query)
        {
            if (!string.IsNullOrEmpty(query) && !string.IsNullOrWhiteSpace(query))
            {
                return await _context.EmployeesEntities.Where(e => e.Name.Contains(query)).Select(e => new LookupModel()
                {
                    Id = e.Id,
                    Name = e.Name
                }).ToListAsync();
            }
            return await _context.EmployeesEntities.Select(e => new LookupModel()
            {
                Id = e.Id,
                Name = e.Name
            }).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> EndService(EndServiceModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await EndEmployeeService(model);
                    if (result)
                    {
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddBlackList(EndServiceModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await EndEmployeeService(model);
                    if (result)
                    {
                        await AddToBlackList(model);
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                }
                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
            }
        }
        private bool EmployeesEntityExists(long id)
        {
            return _context.EmployeesEntities.Any(e => e.Id == id);
        }

        private async Task<bool> EndEmployeeService(EndServiceModel model)
        {
            var employee = await _context.EmployeesEntities.FirstOrDefaultAsync(x => x.Id == model.EmployeeId);
            if (employee == null)
            {
                return false;
            }
            if (model.EndDate == default(DateTime))
            {
                return false;
            }
            employee.EndDate = model.EndDate;
            employee.InsuranceEndDate = model.EndDate;
            employee.EndServiceReasonId = model.Reason;
            employee.Notes += model.Notes;
            employee.IsActive = false;

            if (model.PenaltyAmount >= 0)
            {
                var penalties = new PenaltyEntity()
                {
                    EmployeeId = model.EmployeeId,
                    Amount = model.PenaltyAmount,
                    PenaltyType = 1,
                    Reason = model.PenaltyReason
                };
                await _context.PenaltiesEntity.AddAsync(penalties);
            }
            return true;
        }

        private async Task AddToBlackList(EndServiceModel model)
        {
            var employee = await _context.EmployeesEntities.Include(x => x.Job).FirstOrDefaultAsync(x => x.Id == model.EmployeeId);
            if (employee != null)
            {
                var ser = (await _context.BlackListEntity.OrderByDescending(x => x.Id).FirstOrDefaultAsync());
                var blackListMember = new BlackListEntity()
                {
                    Name = employee.Name,
                    Job = employee.Job.Name,
                    Nat_Id = employee.NationalId,
                    Company = "بايونير",
                    Reason = model.Notes,
                    Ser = ser.Ser + 1
                };
                await _context.BlackListEntity.AddAsync(blackListMember);
            }
        }
    }
}
