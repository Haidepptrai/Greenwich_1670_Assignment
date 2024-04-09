﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FPT_Pharmacy_Assignment.Models;

namespace FPT_Pharmacy_Assignment.Data
{
    public class FPT_Pharmacy_AssignmentContext : DbContext
    {
        public FPT_Pharmacy_AssignmentContext (DbContextOptions<FPT_Pharmacy_AssignmentContext> options)
            : base(options)
        {
        }

        public DbSet<FPT_Pharmacy_Assignment.Models.User> User { get; set; } = default!;
    }
}
