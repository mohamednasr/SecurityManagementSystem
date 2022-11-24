using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Core.Models;
using SecurityMS.Core.Models.Enums;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly AppDbContext _context;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CustomersEntities.Include(c => c.CustomerType).Include(z => z.Government).Include(c => c.ParentCustomers).Where(c => !c.IsDeleted);




            var CustomerType = new List<CustomerTypesLookup>();
            CustomerType.Add(new CustomerTypesLookup() { Id = 0, Name = "أختر نوع العميل" });
            CustomerType.AddRange(await _context.CustomerTypesLookups.ToListAsync());
            ViewData["CustomerTypes"] = new SelectList(CustomerType, "Id", "Name", 0);

            var ParentCustomers = new List<CustomersEntity>();
            ParentCustomers.Add(new CustomersEntity() { Id = 0, Name = "أختر العميل" });
            ParentCustomers.AddRange(await _context.CustomersEntities.Where(c => c.CustomerTypeId == (int)CustomerTypeEnum.Group).ToListAsync());
            ViewData["ParentCustomerId"] = new SelectList(ParentCustomers, "Id", "Name");

            var Governments = new List<GovernmentEntity>();
            Governments.Add(new GovernmentEntity() { Id = 0, Name = "أختر المحافظة" });
            Governments.AddRange(await _context.GovernmentEntities.ToListAsync());
            ViewData["Governments"] = new SelectList(Governments, "Id", "Name");
            var Zones = new List<ZonesEntity>();
            Zones.Add(new ZonesEntity() { Id = 0, Name = "أختر المنطقة" });
            Zones.AddRange(await _context.ZonesEntities.ToListAsync());
            ViewData["Zones"] = new SelectList(Zones, "Id", "Name");
            return View(await appDbContext.ToListAsync());

        }

        public async Task<IActionResult> search(CustomerModel customer)
        {
            var appDbContext = await _context.CustomersEntities.Include(c => c.CustomerType).Include(z => z.Government)
                                        .ToListAsync();
            if (!(string.IsNullOrEmpty(customer.Name) && string.IsNullOrWhiteSpace(customer.Name)))
                appDbContext = appDbContext.Where(c => c.Name.Contains(customer.Name)).ToList();
            if (!(string.IsNullOrEmpty(customer.CommercialNumber) && string.IsNullOrWhiteSpace(customer.CommercialNumber)))
                appDbContext = appDbContext.Where(c => c.CommercialNumber.Contains(customer.CommercialNumber)).ToList();
            if (!(string.IsNullOrEmpty(customer.TaxId) && string.IsNullOrWhiteSpace(customer.TaxId)))
                appDbContext = appDbContext.Where(c => c.TaxId.Contains(customer.TaxId)).ToList();
            //if (customer.ZoneId.HasValue && customer.ZoneId.Value != 0)
            //    appDbContext = appDbContext.Where(c => c.ZoneId == customer.ZoneId).ToList();
            if (customer.GovernmentId.HasValue && customer.GovernmentId.Value != 0)
                appDbContext = appDbContext.Where(c => c.GovernmentId == customer.GovernmentId).ToList();
            if (customer.ParentCustomerId.HasValue && customer.ParentCustomerId.Value != 0)
                appDbContext = appDbContext.Where(c => c.ParentCustomerId == customer.ParentCustomerId).ToList();
            if (customer.CustomerTypeId != 0)
                appDbContext = appDbContext.Where(c => c.CustomerTypeId == customer.CustomerTypeId).ToList();

            var CustomerTyoe = new List<CustomerTypesLookup>();
            CustomerTyoe.Add(new CustomerTypesLookup() { Id = 0, Name = "أختر نوع العميل" });
            CustomerTyoe.AddRange(await _context.CustomerTypesLookups.ToListAsync());
            ViewData["CustomerTypes"] = new SelectList(CustomerTyoe, "Id", "Name", customer.CustomerTypeId);

            var ParentCustomers = new List<CustomersEntity>();
            ParentCustomers.Add(new CustomersEntity() { Id = 0, Name = "أختر العميل" });
            ParentCustomers.AddRange(await _context.CustomersEntities.Where(c => c.CustomerTypeId == (int)CustomerTypeEnum.Group).ToListAsync());
            ViewData["ParentCustomerId"] = new SelectList(ParentCustomers, "Id", "Name", customer.ParentCustomerId);

            var Governments = new List<GovernmentEntity>();
            Governments.Add(new GovernmentEntity() { Id = 0, Name = "أختر المحافظة" });
            Governments.AddRange(await _context.GovernmentEntities.ToListAsync());
            ViewData["Governments"] = new SelectList(Governments, "Id", "Name");
            var Zones = new List<ZonesEntity>();
            Zones.Add(new ZonesEntity() { Id = 0, Name = "أختر المنطقة" });
            Zones.AddRange(await _context.ZonesEntities.Where(c => c.GovernmentId == customer.GovernmentId).ToListAsync());
            ViewData["Zones"] = new SelectList(Zones, "Id", "Name");
            return View("Index", appDbContext);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customersEntity = await _context.CustomersEntities
                .Include(c => c.CustomerType)
                .Include(c => c.ParentCustomers)
                .Include(c => c.Government)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customersEntity == null)
            {
                return NotFound();
            }

            return View(customersEntity);
        }

        // GET: Customers/Create
        public async Task<IActionResult> Create()
        {
            CustomerModel customer = new CustomerModel();

            customer.CustomerTypeId = (int)CustomerTypeEnum.Individual;
            var CustomersList = new List<CustomersEntity>() { new CustomersEntity() { Id = 0, Name = "أختر الشركة" } };
            CustomersList.AddRange(await _context.CustomersEntities.Where(c => c.Id != customer.Id).ToListAsync());
            ViewData["ParentCustomerId"] = new SelectList(CustomersList, "Id", "Name");

            var Governments = new List<GovernmentEntity>();
            Governments.Add(new GovernmentEntity() { Id = 0, Name = "أختر المحافظة" });
            Governments.AddRange(await _context.GovernmentEntities.ToListAsync());
            ViewData["Governments"] = new SelectList(Governments, "Id", "Name");
            var Zones = new List<ZonesEntity>();
            Zones.Add(new ZonesEntity() { Id = 0, Name = "أختر المنطقة" });
            Zones.AddRange(await _context.ZonesEntities.Where(c => c.GovernmentId == customer.GovernmentId).ToListAsync());
            ViewData["Zones"] = new SelectList(Zones, "Id", "Name");
            return View(customer);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CustomerTypeId,ParentCustomerId,CommercialNumber,TaxId,TaxFileNumber,Id,Address,ZoneId,GovernmentId")] CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                CustomersEntity customersEntity = new CustomersEntity()
                {
                    Name = customer.Name,
                    CustomerTypeId = customer.CustomerTypeId,
                    ParentCustomerId = customer.CustomerTypeId == (int)CustomerTypeEnum.Group || customer.ParentCustomerId == 0 ? null : customer.ParentCustomerId,
                    CommercialNumber = customer.CommercialNumber,
                    TaxId = customer.TaxId,
                    GovernmentId = customer.GovernmentId.GetValueOrDefault(0) > 0 ? customer.GovernmentId : null,
                    Address = customer.Address,
                    TaxFileNumber = customer.TaxFileNumber
                };
                _context.Add(customersEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var CustomersList = new List<CustomersEntity>() { new CustomersEntity() { Id = 0, Name = "أختر الشركة" } };
            CustomersList.AddRange(await _context.CustomersEntities.Where(c => c.Id != customer.Id).ToListAsync());
            ViewData["ParentCustomerId"] = new SelectList(CustomersList, "Id", "Name", customer.ParentCustomerId);
            var Governments = new List<GovernmentEntity>();
            Governments.Add(new GovernmentEntity() { Id = 0, Name = "أختر المحافظة" });
            Governments.AddRange(await _context.GovernmentEntities.ToListAsync());
            ViewData["Governments"] = new SelectList(Governments, "Id", "Name", customer.GovernmentId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerEntity = await _context.CustomersEntities.Include(c => c.ParentCustomers).Include(c => c.Government).Where(c => c.Id == id).FirstOrDefaultAsync();
            if (customerEntity == null)
            {
                return NotFound();
            }
            CustomerModel customer = new CustomerModel()
            {
                Id = customerEntity.Id,
                Name = customerEntity.Name,
                CustomerTypeId = customerEntity.CustomerTypeId,
                ParentCustomerId = customerEntity.CustomerTypeId == (int)CustomerTypeEnum.Group || customerEntity.ParentCustomerId == 0 ? null : customerEntity.ParentCustomerId,
                CommercialNumber = customerEntity.CommercialNumber,
                TaxId = customerEntity.TaxId,
                GovernmentId = customerEntity.Government != null ? customerEntity.GovernmentId : 0,
                Address = customerEntity.Address
            };
            var CustomersList = new List<CustomersEntity>() { new CustomersEntity() { Id = 0, Name = "أختر الشركة" } };
            CustomersList.AddRange(await _context.CustomersEntities.Where(c => c.Id != customer.Id).ToListAsync());
            ViewData["ParentCustomerId"] = new SelectList(CustomersList, "Id", "Name", customer.ParentCustomerId);

            var Governments = new List<GovernmentEntity>();
            Governments.Add(new GovernmentEntity() { Id = 0, Name = "أختر المحافظة" });
            Governments.AddRange(await _context.GovernmentEntities.ToListAsync());
            ViewData["Governments"] = new SelectList(Governments, "Id", "Name", customer.GovernmentId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,CustomerTypeId,ParentCustomerId,CommercialNumber,TaxId,TaxFileNumber,Id,Address,ZoneId,GovernmentId")] CustomerModel customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CustomersEntity customersEntity = new CustomersEntity()
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        CustomerTypeId = customer.CustomerTypeId,
                        ParentCustomerId = customer.CustomerTypeId == (int)CustomerTypeEnum.Group || customer.ParentCustomerId == 0 ? null : customer.ParentCustomerId,
                        CommercialNumber = customer.CommercialNumber,
                        TaxId = customer.TaxId,
                        GovernmentId = customer.GovernmentId,
                        Address = customer.Address,
                        TaxFileNumber = customer.TaxFileNumber
                    };
                    _context.Update(customersEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersEntityExists(customer.Id))
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
            ViewData["ParentCustomerId"] = new SelectList(await _context.CustomersEntities.Where(c => c.Id != customer.Id).ToListAsync(), "Id", "Name", customer.ParentCustomerId);
            var Governments = new List<GovernmentEntity>();
            Governments.Add(new GovernmentEntity() { Id = 0, Name = "أختر المحافظة" });
            Governments.AddRange(await _context.GovernmentEntities.ToListAsync());
            ViewData["Governments"] = new SelectList(Governments, "Id", "Name", customer.GovernmentId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customersEntity = await _context.CustomersEntities
              .Include(c => c.CustomerType)
                .Include(c => c.ParentCustomers)
                .Include(c => c.Government)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customersEntity == null)
            {
                return NotFound();
            }

            return View(customersEntity);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var customersEntity = await _context.CustomersEntities.FindAsync(id);
            customersEntity.Delete(HttpContext.User.Identity.Name);
            _context.CustomersEntities.Update(customersEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomersEntityExists(long id)
        {
            return _context.CustomersEntities.Any(e => e.Id == id);
        }
    }
}
