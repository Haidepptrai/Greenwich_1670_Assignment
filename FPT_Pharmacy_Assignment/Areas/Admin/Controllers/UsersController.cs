using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPT_Pharmacy_Assignment.Areas.Admin.Models;
using FPT_Pharmacy_Assignment.Data;
using Microsoft.AspNetCore.Identity;
using FPT_Pharmacy_Assignment.Areas.Identity.Data;

namespace FPT_Pharmacy_Assignment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly FPT_Pharmacy_AssignmentContext _context;
		private UserManager<CustomUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

		public UsersController(FPT_Pharmacy_AssignmentContext context, UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            // Define a constant for the minimum date
            DateTime minDate = new DateTime(1970, 1, 1);

            var usersViewModel = users.Select(c => new UsersViewModel()
            {
                Username = c.UserName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
                Id = c.Id,
                DateOfBirth = c.DateOfBirth ?? minDate, // Use the custom minimum date
                FullName = c.FullName,
                Role = string.Join(",", _userManager.GetRolesAsync(c).Result ?? Enumerable.Empty<string>())
            }).ToList();

            return View(usersViewModel);
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        // GET: Admin/Users/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Email,PhoneNumber")] CustomUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = await _userManager.FindByIdAsync(id);
                    if (currentUser == null)
                    {
                        return NotFound();
                    }

                    currentUser.UserName = user.UserName;
                    currentUser.Email = user.Email;
                    currentUser.PhoneNumber = user.PhoneNumber;

                    // Update user using UserManager
                    await _userManager.UpdateAsync(currentUser);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Delete user using UserManager
            await _userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            throw new NotImplementedException();
        }
    }
}
