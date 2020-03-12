using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;

namespace CMS.Controllers
{
    public class PercelLocationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PercelLocationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PercelLocations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PercelLocation.Include(p => p.Branch);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PercelLocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var percelLocation = await _context.PercelLocation
                .Include(p => p.Branch)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (percelLocation == null)
            {
                return NotFound();
            }

            return View(percelLocation);
        }

        // GET: PercelLocations/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Address");
            return View();
        }

        // POST: PercelLocations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchId,PercelId,Status,ReceivingDate")] PercelLocation percelLocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(percelLocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Address", percelLocation.BranchId);
            return View(percelLocation);
        }

        // GET: PercelLocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var percelLocation = await _context.PercelLocation.SingleOrDefaultAsync(m => m.Id == id);
            if (percelLocation == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Address", percelLocation.BranchId);
            return View(percelLocation);
        }

        // POST: PercelLocations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchId,PercelId,Status,ReceivingDate")] PercelLocation percelLocation)
        {
            if (id != percelLocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(percelLocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PercelLocationExists(percelLocation.Id))
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
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Address", percelLocation.BranchId);
            return View(percelLocation);
        }

        // GET: PercelLocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var percelLocation = await _context.PercelLocation
                .Include(p => p.Branch)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (percelLocation == null)
            {
                return NotFound();
            }

            return View(percelLocation);
        }

        // POST: PercelLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var percelLocation = await _context.PercelLocation.SingleOrDefaultAsync(m => m.Id == id);
            _context.PercelLocation.Remove(percelLocation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PercelLocationExists(int id)
        {
            return _context.PercelLocation.Any(e => e.Id == id);
        }
    }
}
