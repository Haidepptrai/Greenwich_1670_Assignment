using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FPT_Pharmacy_Assignment.Data;
using Microsoft.AspNetCore.Identity;
using FPT_Pharmacy_Assignment.Areas.Identity.Data;
namespace FPT_Pharmacy_Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<FPT_Pharmacy_AssignmentContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("FPT_Pharmacy_AssignmentContext") ?? throw new InvalidOperationException("Connection string 'FPT_Pharmacy_AssignmentContext' not found.")));

            // Register ApplicationDBContext
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

            // Configure Identity
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDBContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            app.Run();
        }
    }
}
