using System;
using hris.Models;
using Microsoft.EntityFrameworkCore;

namespace hris.DB
{
	public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; } = null!;

        public EmployeeContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=employeedb;Username=postgres");
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=employeedb;Username=postgres;Password=пароль_от_postgres");
        }
    }
}

