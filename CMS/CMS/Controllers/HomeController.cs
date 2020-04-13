using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMS.Models;
using Microsoft.AspNetCore.Http;
using CMS.Data;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Dashboard()
        {
            var employeeId = HttpContext.Session.GetInt32("employeeId");
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);

            ViewBag.employeeName = employee.Name;
            ViewBag.totalPercel = _context.Percels.Count(p => p.BranchId == employee.BranchId && p.Status == "Received");
            ViewBag.totalEmployee = _context.Employees.Count(e => e.BranchId == employee.BranchId);

            return View();
        }

        public IActionResult AdminDashboard()
        {
            var employeeId = HttpContext.Session.GetInt32("employeeId");
            var employee = _context.Employees.FirstOrDefault(e => e.Id == employeeId);

            ViewBag.employeeName = employee.Name;
            ViewBag.totalBranch = _context.Branches.Count();
            ViewBag.totalEmployee = _context.Employees.Count();
            ViewBag.totalPercel = _context.Percels.Count();           
            ViewBag.totalSender = _context.Senders.Count();
            ViewBag.totalReceiver = _context.Receivers.Count();
          
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Pricing()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
