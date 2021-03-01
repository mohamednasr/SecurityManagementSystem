using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecurityMS.Infrastructure.Data;
using SecurityMS.Infrastructure.Data.Entities;
using SecurityMS.Presentation.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityMS.Presentation.Web.Controllers
{
    public class BlackListController : Controller
    {
        private readonly AppDbContext _context;
        public BlackListController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BlackList
        public async Task<IActionResult> Index()
        {
            List<BlackListModel> blackList = await _context.BlackListEntity.Select(x => new BlackListModel() { 
                Id = x.Id,
                Ser = x.Ser,
                Name = x.Name,
                Company = x.Company,
                Job = x.Job,
                Nat_Id = x.Nat_Id,
                Reason = x.Reason
            }).ToListAsync();
            return View(blackList);
        }

        public async Task<IActionResult> Search(BlackListModel searchModel)
        {
            List<BlackListModel> blackList = await _context.BlackListEntity.Where(l => l.Nat_Id == searchModel.Nat_Id || l.Name.Contains(searchModel.Name) || l.Company.Contains(searchModel.Company)).Select(x => new BlackListModel()
            {
                Id = x.Id,
                Ser = x.Ser,
                Name = x.Name,
                Company = x.Company,
                Job = x.Job,
                Nat_Id = x.Nat_Id,
                Reason = x.Reason
            }).ToListAsync();
            return View("Index",blackList);
        }

        // GET: BlackList/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blackListEntity = await _context.BlackListEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blackListEntity == null)
            {
                return NotFound();
            }

            return View(blackListEntity);
        }

        // GET: BlackList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlackList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Ser,Name,Company,Job,Nat_Id,Reason,Id")] BlackListEntity blackListEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blackListEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blackListEntity);
        }

        // GET: BlackList/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blackListEntity = await _context.BlackListEntity.FindAsync(id);
            if (blackListEntity == null)
            {
                return NotFound();
            }
            return View(blackListEntity);
        }

        // POST: BlackList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Ser,Name,Company,Job,Nat_Id,Reason,Id")] BlackListEntity blackListEntity)
        {
            if (id != blackListEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blackListEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlackListEntityExists(blackListEntity.Id))
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
            return View(blackListEntity);
        }

        // GET: BlackList/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blackListEntity = await _context.BlackListEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blackListEntity == null)
            {
                return NotFound();
            }

            return View(blackListEntity);
        }

        // POST: BlackList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var blackListEntity = await _context.BlackListEntity.FindAsync(id);
            _context.BlackListEntity.Remove(blackListEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlackListEntityExists(long id)
        {
            return _context.BlackListEntity.Any(e => e.Id == id);
        }
    }
}
