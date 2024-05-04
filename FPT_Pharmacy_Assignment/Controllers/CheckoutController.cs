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

        [HttpPost]
        public async Task<IActionResult> CompleteCheckout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Update user information
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    user.UserName = model.User.UserName;
                    user.Address = model.User.Address;
                    user.PhoneNumber = model.User.PhoneNumber;

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        // Handle the case where user update fails
                        AddErrors(result);
                        return View(model);
                    }
                }

                // Create new order
                var order = new Order
                {
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Status = "Pending" // Default status
                };
                _context.Order.Add(order);
                await _context.SaveChangesAsync();

                // Create order details
                foreach (var item in model.CartItems)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };
                    _context.OrderDetail.Add(orderDetail);
                }
                await _context.SaveChangesAsync();

                // Redirect to a confirmation page or similar
                return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
            }

            // If the model state is not valid, return to the form with the current model
            return View("Index", model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

    }
}
