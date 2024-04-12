using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FPT_Pharmacy_Assignment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FPT_Pharmacy_Assignment.Data
{
    public class FPT_Pharmacy_AssignmentContext : DbContext
    {
        public FPT_Pharmacy_AssignmentContext (DbContextOptions<FPT_Pharmacy_AssignmentContext> options)
            : base(options)
        {
        }

        public DbSet<FPT_Pharmacy_Assignment.Models.User> User { get; set; } = default!;

        public DbSet<FPT_Pharmacy_Assignment.Models.Product> Product { get; set; } = default!;

        public DbSet<FPT_Pharmacy_Assignment.Models.Order> Order { get; set; } = default!;

        public DbSet<FPT_Pharmacy_Assignment.Models.OrderDetail> OrderDetail { get; set; } = default!;

        public DbSet<FPT_Pharmacy_Assignment.Models.Transaction> Transaction { get; set; } = default!;
    }
}
