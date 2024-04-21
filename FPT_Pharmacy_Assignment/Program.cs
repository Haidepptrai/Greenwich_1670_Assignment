using FPT_Pharmacy_Assignment.Areas.Identity.Data;
using FPT_Pharmacy_Assignment.Data;
using Microsoft.EntityFrameworkCore;
namespace FPT_Pharmacy_Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("FPT_Pharmacy_AssignmentContextConnection") ?? throw new InvalidOperationException("Connection string 'FPT_Pharmacy_AssignmentContextConnection' not found.");

            builder.Services.AddDbContext<FPT_Pharmacy_AssignmentContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<CustomUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<FPT_Pharmacy_AssignmentContext>();

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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );
            });
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();
            app.Run();
        }
    }
}
