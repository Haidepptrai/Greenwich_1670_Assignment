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

        // POST: Admin/Products/FilterProducts
        [HttpPost]
        public IActionResult FilterProducts(List<string> prices)
        {
            var filteredProducts = _context.Product.Where(p =>
                prices.Contains("price1") && p.Price >= 0 && p.Price <= 50 ||
                prices.Contains("price2") && p.Price > 50 && p.Price <= 200 ||
                prices.Contains("price3") && p.Price >= 200 && p.Price <= 500 ||
                prices.Contains("price4") && p.Price > 500 && p.Price <= 1000 ||
                prices.Contains("price5") && p.Price >= 1000 && p.Price <= 2000 ||
                prices.Contains("price6") && p.Price > 2000 && p.Price <= 5000
            ).ToList();

            return PartialView("Index", filteredProducts);
        }
        [HttpGet("Products")]
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
