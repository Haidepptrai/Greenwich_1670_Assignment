using FPT_Pharmacy_Assignment.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FPT_Pharmacy_Assignment.Data;

public class FPT_Pharmacy_AssignmentContext : IdentityDbContext<CustomUser>
{
    public FPT_Pharmacy_AssignmentContext(DbContextOptions<FPT_Pharmacy_AssignmentContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
