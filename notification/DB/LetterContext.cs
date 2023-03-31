using System;
using Microsoft.EntityFrameworkCore;
using notification.Model;

namespace notification.DB
{
	public class LetterContext : DbContext
	{
		public DbSet<Letter> Letters { get; set; } = null!;

		public LetterContext()
		{
			Database.EnsureCreated();
		}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=notifications;Username=postgres");
            
        }
    }
}

