using FPT_Pharmacy_Assignment.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FPT_Pharmacy_Assignment.Areas.Admin.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = serviceProvider.GetRequiredService<FPT_Pharmacy_AssignmentContext>();

            // Seed roles
            string[] roleNames = { "Admin", "Moderator", "User" };
            foreach (var roleName in roleNames)
            {
                // If the role doesn't exist, create it
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed manufacturers if empty
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

            // Seed categories if empty
            if (!context.Category.Any())
            {
                context.Category.AddRange(
                    new Category { Name = "Category 1" },
                    new Category { Name = "Category 2" },
                    new Category { Name = "Category 3" }
                );
                await context.SaveChangesAsync();
            }

            // Seed products if empty
            if (!context.Product.Any())
            {
                context.Product.AddRange(
                    new Product
                    {
                        ManufacturerId = 1,
                        CategoryId = 1,
                        ImageFile = "72cbf20f-3e2a-4510-aa7d-34a4659f7f6c.webp",
                        Name = "Viên nhai Borne Mineral New Nordic hỗ trợ phát triển xương, giúp tăng chiều cao, tăng đề kháng (120 viên)",
                        Description = "Borne Mineral hỗ trợ sự phát triển của xương giúp tăng trưởng chiều cao, giảm nguy cơ còi xương thấp bé, phòng ngừa thiếu canxi ở trẻ em và hỗ trợ tăng cường sức đề kháng.",
                        Price = 10.99m,
                        StockLevel = 100
                    },
                    new Product
                    {
                        ManufacturerId = 1,
                        CategoryId = 2,
                        ImageFile = "880d0dc5-8b82-4bf4-97dc-f113b3f54637",
                        Name = "Kem chống nắng giảm nhờn Eucerin Sun Dry Touch Oil Control Face SPF50+ PA+++ (50ml)",
                        Description = "Kem chống nắng Eucerin Sun Dry Touch Oil Control Face SPF50+ PA+++ với màng lọc tia UVA, UVB tối đa cùng SPF50+ giúp bảo vệ da tối ưu ngăn ngừa các tác hại của ánh nắng mặt trời. Sản phẩm đặc biệt phù hợp để kiểm soát nhờn dành cho da nhờn mụn. Không bổ sung mùi và parabens.",
                        Price = 20.99m,
                        StockLevel = 200
                    },
                    new Product
                    {
                        ManufacturerId = 2,
                        CategoryId = 3,
                        ImageFile = "94e1c906-e08c-46f1-99a2-99f08d03cf7e.webp",
                        Name = "Siro WellKid Multi-Vitamin Liquid bổ sung vitamin, khoáng chất cho trẻ em (150ml)",
                        Description = "Wellkid Multi-Vitamin Liquid bổ sung vitamin, khoáng chất cho trẻ em, giúp tăng cường sức khỏe, nâng cao sức đề kháng.",
                        Price = 30.99m,
                        StockLevel = 300
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
