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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }
        [HttpGet("Page")]
        public IActionResult Index(int page = 1, int pageSize = 9)
        {
            // Calculate the total number of products
            var totalProducts = _context.Product.Count();

            // Calculate the total number of pages
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            // Retrieve products for the current page
            var products = _context.Product
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Pass products and pagination information to the view
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(products);
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
