using FPT_Pharmacy_Assignment.Areas.Identity.Data;
using FPT_Pharmacy_Assignment.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPT_Pharmacy_Assignment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly FPT_Pharmacy_AssignmentContext _context;
        private UserManager<CustomUser> _userManager;

        public DashboardController(FPT_Pharmacy_AssignmentContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }

    }
}
