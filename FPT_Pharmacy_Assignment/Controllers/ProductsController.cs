using FPT_Pharmacy_Assignment.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPT_Pharmacy_Assignment.Controllers
{
    public class ProductsController : Controller
    {
        private readonly FPT_Pharmacy_AssignmentContext _context;

        public ProductsController(FPT_Pharmacy_AssignmentContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
