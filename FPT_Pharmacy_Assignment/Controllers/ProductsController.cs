using FPT_Pharmacy_Assignment.Areas.Admin.Models;
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
        public IActionResult ProductByCategory(int categoryId)
        {
            var productsByCategory = _context.Product.Where(p => p.CategoryId == categoryId).ToList();

            var model = new ProductViewModel
            {
                Products = productsByCategory,
                Categories = _context.Category.ToList()
            };

            return View("Index", model);
        }

        public IActionResult SearchByName(ProductViewModel model)
        {
            var filteredProducts = _context.Product
                .Where(p => string.IsNullOrEmpty(model.searchByName) || p.Name.Contains(model.searchByName))
                .ToList();

            var viewModel = new ProductViewModel
            {
                Products = filteredProducts,
                Categories = _context.Category.ToList()
            };

            return View("Index", viewModel);
        }

        [HttpGet("Products")]
        public IActionResult Index(int page = 1, int pageSize = 9)
        {
            // Calculate the total number of products
            var totalProducts = _context.Product.Count();

            // Calculate the total number of pages
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            var categories = _context.Category;
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
            var viewModel = new ProductViewModel(products, categories);

            return View(viewModel);
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