using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data;
using CMS.Models;
using CMS.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ReceivePercel()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReceivePercel([Bind("SenderName,SenderAddress,SenderEmail,SenderContact,ReceiverName,ReceiverAddress,ReceiverEmail,ReceiverContact,Weight,Cost")] PercelReceive percelReceive)
        {
            if (ModelState.IsValid)
            {
                Sender sender = new Sender() {
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

                Percel percel = new Percel() {
                    Weight = percelReceive.Weight,
                    Cost = percelReceive.Cost,
                    ReceivingDate = System.DateTime.Now,
                    SenderId = sender.Id,
                    ReceiverId = receiver.Id
                };

                _context.Add(percel);
                await _context.SaveChangesAsync();

                var employeeId = HttpContext.Session.GetInt32("employeeId");
                var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);
                PercelLocation percelLocation = new PercelLocation()
                {
                    BranchId = employee.BranchId,
                    PercelId = percel.Id,
                    Status = "Received",
                    ReceivingDate = System.DateTime.Now
                };

                _context.Add(percelLocation);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Report));
            }
            return View(percelReceive);
        }

        public IActionResult Report()
        {
            return View();
        }
    }
}