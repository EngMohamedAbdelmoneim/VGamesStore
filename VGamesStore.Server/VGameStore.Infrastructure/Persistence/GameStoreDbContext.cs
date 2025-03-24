using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using VGameStore.Core.Entities;

namespace VGameStore.Infrastructure.Persistence
{
	public class GameStoreDbContext : DbContext
	{
		public GameStoreDbContext(DbContextOptions options) : base(options)
		{
		}
		public GameStoreDbContext() { }

		// Database Taples ...... 
		public DbSet<Game> Games { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
