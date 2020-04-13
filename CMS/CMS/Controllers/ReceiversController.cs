using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Controllers
{
    [Authorize]
    public class ReceiversController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceiversController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Receivers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Receivers.ToListAsync());
        }

        // GET: Receivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiver = await _context.Receivers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (receiver == null)
            {
                return NotFound();
            }

            return View(receiver);
        }

        // GET: Receivers/Edit/5
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiver = await _context.Receivers.SingleOrDefaultAsync(m => m.Id == id);
            if (receiver == null)
            {
                return NotFound();
            }
            return View(receiver);
        }

        // POST: Receivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Email,Contact")] Receiver receiver)
        {
            if (id != receiver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receiver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiverExists(receiver.Id))
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
            return View(receiver);
        }

        // GET: Receivers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receiver = await _context.Receivers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (receiver == null)
            {
                return NotFound();
            }

            return View(receiver);
        }

        // POST: Receivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receiver = await _context.Receivers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Receivers.Remove(receiver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiverExists(int id)
        {
            return _context.Receivers.Any(e => e.Id == id);
        }
    }
}
