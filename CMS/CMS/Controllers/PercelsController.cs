using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Http;

namespace CMS.Controllers
{
    public class PercelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PercelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Percels
        public async Task<IActionResult> Index()
        {
            var employeeId = HttpContext.Session.GetInt32("employeeId");
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);

            var percels = _context.Percels
                .Include(p => p.Branch)
                .Include(p => p.Receiver)
                .Include(p => p.Sender)
                .Where(p => p.BranchId == employee.BranchId);
            return View(await percels.ToListAsync());
        }

        // GET: Percels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var percel = await _context.Percels
                .Include(p => p.Branch)
                .Include(p => p.Receiver)
                .Include(p => p.Sender)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (percel == null)
            {
                return NotFound();
            }

            return View(percel);
        }

        // GET: Percels/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name");
            ViewData["ReceiverId"] = new SelectList(_context.Receivers, "Id", "Name");
            ViewData["SenderId"] = new SelectList(_context.Senders, "Id", "Name");
            return View();
        }

        // POST: Percels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Weight,Cost,ReceivingDate,SenderId,ReceiverId,BranchId,Status")] Percel percel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(percel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", percel.BranchId);
            ViewData["ReceiverId"] = new SelectList(_context.Receivers, "Id", "Name", percel.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Senders, "Id", "Name", percel.SenderId);
            return View(percel);
        }

        // GET: Percels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var percel = await _context.Percels.SingleOrDefaultAsync(m => m.Id == id);
            if (percel == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", percel.BranchId);
            ViewData["ReceiverId"] = new SelectList(_context.Receivers, "Id", "name", percel.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Senders, "Id", "name", percel.SenderId);
            return View(percel);
        }

        // POST: Percels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Weight,Cost,ReceivingDate,SenderId,ReceiverId,BranchId,Status")] Percel percel)
        {
            if (id != percel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(percel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PercelExists(percel.Id))
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
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", percel.BranchId);
            ViewData["ReceiverId"] = new SelectList(_context.Receivers, "Id", "Name", percel.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Senders, "Id", "Name", percel.SenderId);
            return View(percel);
        }

        public async Task<IActionResult> Deliver(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var percel = await _context.Percels.SingleOrDefaultAsync(m => m.Id == id);
            if (percel == null)
            {
                return NotFound();
            }

            percel.Status = "Delivered";

            _context.Update(percel);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Percels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var percel = await _context.Percels
                .Include(p => p.Branch)
                .Include(p => p.Receiver)
                .Include(p => p.Sender)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (percel == null)
            {
                return NotFound();
            }

            return View(percel);
        }

        // POST: Percels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var percel = await _context.Percels.SingleOrDefaultAsync(m => m.Id == id);
            _context.Percels.Remove(percel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PercelExists(int id)
        {
            return _context.Percels.Any(e => e.Id == id);
        }
    }
}
