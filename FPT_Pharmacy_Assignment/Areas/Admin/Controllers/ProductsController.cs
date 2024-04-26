using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPT_Pharmacy_Assignment.Areas.Admin.Models;
using FPT_Pharmacy_Assignment.Data;
using Microsoft.Extensions.Hosting;

namespace FPT_Pharmacy_Assignment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly FPT_Pharmacy_AssignmentContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(FPT_Pharmacy_AssignmentContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }
        
        // GET: Admin/Products/Details/5
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

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewBag.ManufacturerId = new SelectList(_context.Manufacturer, "ManufacturerId", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ManufacturerId,Name,Description,Price,StockLevel")] Product product, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/products");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName); // Generate a unique file name
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                product.ImageFile = fileName;
            }

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { area = "Admin" });
        }


        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturer.FindAsync(product.ManufacturerId);
            if (manufacturer == null)
            {
                return NotFound();
            }

            ViewData["ManufacturerName"] = manufacturer.Name;
            return View(product);
        }


        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ManufacturerId,Name,Description,Price,StockLevel")] Product product, IFormFile imageFile)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            try
            {
                // Load the existing product entity from the context
                var existingProduct = await _context.Product.FindAsync(id);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                // If a new image file is uploaded, handle it
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads/products");
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName); // Generate a unique file name
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // Delete the previous image file if necessary
                    if (!string.IsNullOrEmpty(existingProduct.ImageFile))
                    {
                        var previousFilePath = Path.Combine(_environment.WebRootPath, "uploads/products", existingProduct.ImageFile);
                        if (System.IO.File.Exists(previousFilePath))
                        {
                            System.IO.File.Delete(previousFilePath);
                        }
                    }

                    // Update the product's image file
                    existingProduct.ImageFile = fileName;
                }

                // Update other properties
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.StockLevel = product.StockLevel;

                // Save changes
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { area = "Admin" });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { area = "Admin" });
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
