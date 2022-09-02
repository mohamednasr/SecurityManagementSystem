using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    [Authorize]
    public class UniformsController : Controller
    {
        private readonly AppDbContext _context;

        public UniformsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Uniforms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Uniform.ToListAsync());
        }

        // GET: Uniforms/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uniformEntity = await _context.Uniform
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uniformEntity == null)
            {
                return NotFound();
            }

            return View(uniformEntity);
        }

        // GET: Uniforms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uniforms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name,Size,TotalCount,AvailableTotalCount,MinimumAlert,Id")] UniformEntity uniformEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uniformEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uniformEntity);
        }

        // GET: Uniforms/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uniformEntity = await _context.Uniform.FindAsync(id);
            if (uniformEntity == null)
            {
                return NotFound();
            }
            return View(uniformEntity);
        }

        // POST: Uniforms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Code,Name,Size,TotalCount,AvailableTotalCount,MinimumAlert,Id")] UniformEntity uniformEntity)
        {
            if (id != uniformEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uniformEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniformEntityExists(uniformEntity.Id))
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
            return View(uniformEntity);
        }

        // GET: Uniforms/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uniformEntity = await _context.Uniform
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uniformEntity == null)
            {
                return NotFound();
            }

            return View(uniformEntity);
        }

        // POST: Uniforms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var uniformEntity = await _context.Uniform.FindAsync(id);
            _context.Uniform.Remove(uniformEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniformEntityExists(long id)
        {
            return _context.Uniform.Any(e => e.Id == id);
        }
    }
}
