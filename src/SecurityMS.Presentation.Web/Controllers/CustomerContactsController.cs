using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class CustomerContactsController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerContactsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: CustomerContacts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CustomerContactsEntities.Include(c => c.Customer).Where(c => !c.IsDeleted);
            return View(await appDbContext.ToListAsync());
        }

        // GET: CustomerContacts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerContactsEntity = await _context.CustomerContactsEntities
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerContactsEntity == null)
            {
                return NotFound();
            }

            return View(customerContactsEntity);
        }

        // GET: CustomerContacts/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.CustomersEntities, "Id", "Name");
            return View();
        }

        // POST: CustomerContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Job,CustomerId,Email,Phone,Id")] CustomerContactsEntity customerContactsEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerContactsEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomersEntities, "Id", "Name", customerContactsEntity.CustomerId);
            return View(customerContactsEntity);
        }

        // GET: CustomerContacts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerContactsEntity = await _context.CustomerContactsEntities.FindAsync(id);
            if (customerContactsEntity == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomersEntities, "Id", "Name", customerContactsEntity.CustomerId);
            return View(customerContactsEntity);
        }

        // POST: CustomerContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,Job,CustomerId,Email,Phone,Id")] CustomerContactsEntity customerContactsEntity)
        {
            if (id != customerContactsEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerContactsEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerContactsEntityExists(customerContactsEntity.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.CustomersEntities, "Id", "Name", customerContactsEntity.CustomerId);
            return View(customerContactsEntity);
        }

        // GET: CustomerContacts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerContactsEntity = await _context.CustomerContactsEntities
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerContactsEntity == null)
            {
                return NotFound();
            }

            return View(customerContactsEntity);
        }

        // POST: CustomerContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var customerContactsEntity = await _context.CustomerContactsEntities.FindAsync(id);
            customerContactsEntity.Delete(HttpContext.User.Identity.Name);
            _context.CustomerContactsEntities.Update(customerContactsEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerContactsEntityExists(long id)
        {
            return _context.CustomerContactsEntities.Any(e => e.Id == id);
        }
    }
}
