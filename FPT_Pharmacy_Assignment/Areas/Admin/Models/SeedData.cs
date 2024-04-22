using FPT_Pharmacy_Assignment.Data;
using Microsoft.AspNetCore.Identity;

namespace FPT_Pharmacy_Assignment.Areas.Admin.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<FPT_Pharmacy_AssignmentContext>();

            string[] roleNames = ["Admin", "Moderator", "User"];
            foreach (var roleName in roleNames)
            {
                // If the role doesn't exist, create it
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            if (!context.Manufacturer.Any())
            {
                context.Manufacturer.AddRange(
                    new Manufacturer
                    {
                        Name = "Manufacturer 1",
                        Phone = "1234567890"
                    },

                    new Manufacturer
                    {
                        Name = "Manufacturer 2",
                        Phone = "9876543210"
                    },

                    new Manufacturer
                    {
                        Name = "Manufacturer 3",
                        Phone = "5555555555"
                    }
                );
                await context.SaveChangesAsync();
            }


            // Look for any products.
            if (!context.Product.Any())
            {
                context.Product.AddRange(
                    new Product
                    {
                        ManufacturerId = 1,
                        ImageFile = "image1.jpg",
                        Name = "Product 1",
                        Description = "Description for Product 1",
                        Price = 10.99m,
                        StockLevel = 100
                    },

                    new Product
                    {
                        ManufacturerId = 1,
                        ImageFile = "image2.jpg",
                        Name = "Product 2",
                        Description = "Description for Product 2",
                        Price = 20.99m,
                        StockLevel = 200
                    },

                    new Product
                    {
                        ManufacturerId = 1,
                        ImageFile = "image3.jpg",
                        Name = "Product 3",
                        Description = "Description for Product 3",
                        Price = 30.99m,
                        StockLevel = 300
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
