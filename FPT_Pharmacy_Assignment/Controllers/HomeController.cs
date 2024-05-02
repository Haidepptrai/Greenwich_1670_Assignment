using FPT_Pharmacy_Assignment.Data;
using FPT_Pharmacy_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FPT_Pharmacy_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly FPT_Pharmacy_AssignmentContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, FPT_Pharmacy_AssignmentContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CartPage()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
