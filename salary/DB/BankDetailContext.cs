using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using salary.Models;

namespace salary.DB
{
	public class BankDetailContext : DbContext
    {
        public DbSet<BankDetail> BankDetails { get; set; } = null!;

        public BankDetailContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=postgre-postgres;Port=5432;Database=salary;Username=postgres");
        }
    }
}

