using FPT_Pharmacy_Assignment.Areas.Identity.Data;
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
    }

    public DbSet<FPT_Pharmacy_Assignment.Areas.Admin.Models.User> User { get; set; } = default!;

    public DbSet<Manufacturer> Manufacturer { get; set; } = default!;

    public DbSet<FPT_Pharmacy_Assignment.Areas.Admin.Models.Order> Order { get; set; } = default!;

    public DbSet<FPT_Pharmacy_Assignment.Areas.Admin.Models.OrderDetail> OrderDetail { get; set; } = default!;

    public DbSet<FPT_Pharmacy_Assignment.Areas.Admin.Models.Product> Product { get; set; } = default!;

    public DbSet<Category> Category { get; set; } = default!;
}

