using System;
using lms.Models;
using Microsoft.EntityFrameworkCore;

namespace lms.DB
{
    public class CourseContext : DbContext
    {
        public DbSet<Course> Courses { get; set; } = null!;

        public CourseContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=postgre-postgres;Port=5432;Database=lms;Username=postgres");
        }
    }
}

