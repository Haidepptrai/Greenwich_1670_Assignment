using FPT_Pharmacy_Assignment.Areas.Admin.Models;
using FPT_Pharmacy_Assignment.Areas.Identity.Data;
using FPT_Pharmacy_Assignment.Data;
using FPT_Pharmacy_Assignment.Extensions;
using FPT_Pharmacy_Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FPT_Pharmacy_Assignment.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly FPT_Pharmacy_AssignmentContext _context;


        public CheckoutController(UserManager<CustomUser> userManager, FPT_Pharmacy_AssignmentContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();

            if (cart.Count == 0)
            {
                // Display toastr warning
                TempData["toastrWarning"] = "Your cart is empty.";
                return RedirectToAction("Index", "Cart");
            }

            var viewModel = new CheckoutViewModel
            {
                CartItems = cart,
                User = user
            };

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult OrderComplete()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.OrderId = TempData["OrderId"];  // This will be null if no orderId was set
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrderCompleteAsync(CustomUser userInfo)
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");
            if (cart == null || !cart.Any())
            {
                TempData["Message"] = "Your cart is empty.";
                return RedirectToAction("OrderComplete");
            }

            if (!ModelState.IsValid)
            {
                // Automatically picks up model errors from data annotations
                return View("Index");
            }

            // Calculate total price including delivery
            decimal totalPrice = cart.Sum(item => item.Price * item.Quantity) + 10; // Assuming delivery charge is $10

            // Create and save the order record
            var order = new Order
            {
                UserId = userInfo.Id,
                CreatedAt = DateTime.Now,
                Status = "Pending",
                TotalPrice = totalPrice,
            };
            _context.Order.Add(order);
            _context.SaveChanges();

            // Save each cart item linked to the order
            foreach (var item in cart)
            {
                var orderItem = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                _context.OrderDetail.Add(orderItem);
            }
            _context.SaveChanges();

            // Clear the cart after saving the order
            HttpContext.Session.Remove("Cart");

            // Set TempData and redirect
            TempData["Message"] = "Your order has been placed and is being processed. You will receive an email shortly!";
            TempData["OrderId"] = order.OrderId;
            return RedirectToAction("OrderComplete");
        }
    }
}
