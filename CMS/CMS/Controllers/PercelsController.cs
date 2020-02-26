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
            var applicationDbContext = _context.Percels.Include(p => p.Receiver).Include(p => p.Sender);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Percels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var percel = await _context.Percels
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
            ViewData["ReceiverId"] = new SelectList(_context.Receivers, "Id", "Address");
            ViewData["SenderId"] = new SelectList(_context.Senders, "Id", "Address");
            return View();
        }

        // POST: Percels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Weight,ReceivingDate,SenderId,ReceiverId")] Percel percel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(percel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReceiverId"] = new SelectList(_context.Receivers, "Id", "Address", percel.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Senders, "Id", "Address", percel.SenderId);
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
            ViewData["ReceiverId"] = new SelectList(_context.Receivers, "Id", "Address", percel.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Senders, "Id", "Address", percel.SenderId);
            return View(percel);
        }

        // POST: Percels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Weight,ReceivingDate,SenderId,ReceiverId")] Percel percel)
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
            ViewData["ReceiverId"] = new SelectList(_context.Receivers, "Id", "Address", percel.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Senders, "Id", "Address", percel.SenderId);
            return View(percel);
        }

        // GET: Percels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var percel = await _context.Percels
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
