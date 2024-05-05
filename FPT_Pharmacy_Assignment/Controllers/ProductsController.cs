using FPT_Pharmacy_Assignment.Data;
using FPT_Pharmacy_Assignment.Extensions;
using FPT_Pharmacy_Assignment.Models;
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
            var products = _context.Product;
            return View(new ProductViewModel(products));
        }

        // POST: Admin/Products/FilterProducts
        [HttpPost]
        public IActionResult FilterProducts(List<string> prices)
        {
            var filteredProducts = _context.Product.AsQueryable();

            if (prices != null && prices.Any())
            {
                filteredProducts = filteredProducts.Where(p =>
                    (prices.Contains("price1") && p.Price >= 0 && p.Price <= 50) ||
                    (prices.Contains("price2") && p.Price > 50 && p.Price <= 200) ||
                    (prices.Contains("price3") && p.Price >= 200 && p.Price <= 500) ||
                    (prices.Contains("price4") && p.Price > 500 && p.Price <= 1000) ||
                    (prices.Contains("price5") && p.Price >= 1000 && p.Price <= 2000) ||
                    (prices.Contains("price6") && p.Price > 2000 && p.Price <= 5000)
                );
            }

            var filteredProductList = filteredProducts.ToList();

            return View("Products", filteredProductList);
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
                .Include(p => p.Manufacturer)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        public IActionResult AddToCart([FromBody] CartItem model)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            var cartItem = cart.Find(item => item.ProductId == model.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity += model.Quantity;
                HttpContext.Session.Set("Cart", cart);
                return Json(new { success = true, message = "Your product quantity has been updated." });
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    Price = model.Price,
                    Quantity = model.Quantity
                });
                HttpContext.Session.Set("Cart", cart);
                return Json(new { success = true, message = "Product added to cart successfully!" });
            }
        }
    }
}
