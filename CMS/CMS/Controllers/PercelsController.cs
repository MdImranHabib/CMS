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
using CMS.Models.ViewModels;

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
            
           ViewBag.Percels =await _context.Percels
                .Include(p => p.Branch)
                .Include(p => p.Receiver)
                .Include(p => p.Sender)
                .Where(p => p.BranchId == employee.BranchId && p.Status == "Received").ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Code)
        {
            var employeeId = HttpContext.Session.GetInt32("employeeId");
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (Code == null)
            {
                ViewBag.Percels = await _context.Percels
                .Include(p => p.Branch)
                .Include(p => p.Receiver)
                .Include(p => p.Sender)
                .Where(p => p.BranchId == employee.BranchId && p.Status == "Received").ToListAsync();
                return View();
            }

            var percel = await _context.Percels.SingleOrDefaultAsync(p => p.Code == Code);
            if (percel == null)
            {
                ViewBag.Percels = await _context.Percels
                .Include(p => p.Branch)
                .Include(p => p.Receiver)
                .Include(p => p.Sender)
                .Where(p => p.BranchId == employee.BranchId && p.Status == "Received").ToListAsync();
                return View();
            }

            percel.BranchId = employee.BranchId;
            percel.Status = "Received";

            _context.Update(percel);
            await _context.SaveChangesAsync();

            PercelLocation percelLocation = new PercelLocation()
            {
                PercelId = percel.Id,
                BranchId = employee.BranchId,
                Status = "Received",
                ReceivingDate = System.DateTime.Now
            };

            _context.Add(percelLocation);
            await _context.SaveChangesAsync();

            ViewBag.Percels = await _context.Percels
                 .Include(p => p.Branch)
                 .Include(p => p.Receiver)
                 .Include(p => p.Sender)
                 .Where(p => p.BranchId == employee.BranchId && p.Status == "Received").ToListAsync();

            return View();
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
        //public IActionResult Create()
        //{
        //    ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name");
        //    ViewData["ReceiverId"] = new SelectList(_context.Receivers, "Id", "Name");
        //    ViewData["SenderId"] = new SelectList(_context.Senders, "Id", "Name");
        //    return View();
        //}

        //// POST: Percels/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Weight,Cost,ReceivingDate,SenderId,ReceiverId,BranchId,Status")] Percel percel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(percel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", percel.BranchId);
        //    ViewData["ReceiverId"] = new SelectList(_context.Receivers, "Id", "Name", percel.ReceiverId);
        //    ViewData["SenderId"] = new SelectList(_context.Senders, "Id", "Name", percel.SenderId);
        //    return View(percel);
        //}

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
            ViewData["ReceiverId"] = new SelectList(_context.Receivers, "Id", "Name", percel.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Senders, "Id", "Name", percel.SenderId);
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

        public IActionResult ReceivePercelfromUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReceivePercelfromUser([Bind("SenderName,SenderAddress,SenderEmail,SenderContact,ReceiverName,ReceiverAddress,ReceiverEmail,ReceiverContact,Weight,Cost")] PercelReceive percelReceive)
        {
            if (ModelState.IsValid)
            {
                Sender sender = new Sender()
                {
                    Name = percelReceive.SenderName,
                    Address = percelReceive.SenderAddress,
                    Email = percelReceive.SenderEmail,
                    Contact = percelReceive.SenderContact
                };

                _context.Add(sender);
                await _context.SaveChangesAsync();

                Receiver receiver = new Receiver()
                {
                    Name = percelReceive.ReceiverName,
                    Address = percelReceive.ReceiverAddress,
                    Email = percelReceive.ReceiverEmail,
                    Contact = percelReceive.ReceiverContact

                };

                _context.Add(receiver);
                await _context.SaveChangesAsync();

                var employeeId = HttpContext.Session.GetInt32("employeeId");
                var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);
                Percel percel = new Percel()
                {
                    Code = GetPercelCode(employee),
                    Weight = percelReceive.Weight,
                    Cost = percelReceive.Cost,
                    ReceivingDate = System.DateTime.Now,
                    SenderId = sender.Id,
                    ReceiverId = receiver.Id,
                    BranchId = employee.BranchId,
                    Status = "Received"
                };

                _context.Add(percel);
                await _context.SaveChangesAsync();

                PercelLocation percelLocation = new PercelLocation()
                {
                    PercelId = percel.Id,
                    BranchId = employee.BranchId,
                    Status = "Received",
                    ReceivingDate = System.DateTime.Now
                };

                _context.Add(percelLocation);
                await _context.SaveChangesAsync();

                return RedirectToAction("Report", percel);
            }
            return View(percelReceive);
        }

        private string GetPercelCode(Employee employee)
        {
            var currentYear = System.DateTime.Now.Year;

            var percelCount = _context.Percels.Count(x => (x.BranchId == employee.BranchId) && (x.ReceivingDate.Year == currentYear)) + 1;

            var branch = _context.Branches.FirstOrDefault(x => x.Id == employee.BranchId);

            string leadingZero = "";
            int length = 3 - percelCount.ToString().Length;
            for (int i = 0; i < length; i++)
            {
                leadingZero += "0";
            }

            string percelCodeNo = branch.Name + "-" + currentYear + "-" + leadingZero + percelCount;
            return percelCodeNo;
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

            PercelLocation percelLocation = new PercelLocation()
            {
                PercelId = percel.Id,
                BranchId = percel.BranchId,
                Status = "Delivered",
                ReceivingDate = System.DateTime.Now
            };

            _context.Add(percelLocation);
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

        public async Task<IActionResult> Report(Percel percel)
        {
            if(percel == null)
            {
                return Content("There is no Percel Received!");
            }

            var aPercel = await _context.Percels
                .Include(p => p.Branch)
                .Include(p => p.Sender)
                .Include(p => p.Receiver)
                .SingleOrDefaultAsync(p => p.Id == percel.Id);

            if(aPercel == null)
            {
                return Content("There is no Percel Received!");
            }

            return View(aPercel);
        }

        private bool PercelExists(int id)
        {
            return _context.Percels.Any(e => e.Id == id);
        }
    }
}
