using System;
using hris.Models;
using Microsoft.EntityFrameworkCore;

namespace hris.DB
{
	public class DataBaseService : DbContext
    {
        public DbSet<CreateVacantPositionRequest> CreateVacantPositionRequests { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

        public DataBaseService()
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

