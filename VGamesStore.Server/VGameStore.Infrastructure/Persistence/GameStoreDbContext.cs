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
		public DbSet<Genre> Genres { get; set; }
		public DbSet<GameGenre> GameGenre { get; set; }
		public DbSet<GameImage> GameImage { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<GameGenre>()
				 .HasKey(gg => new { gg.GameId, gg.GenreId });

			modelBuilder.Entity<GameGenre>()
				.HasOne(gg => gg.Game)
				.WithMany(g => g.GameGenres)
				.HasForeignKey(gg => gg.GameId);

			modelBuilder.Entity<GameGenre>()
				.HasOne(gg => gg.Genre)
				.WithMany(g => g.GameGenres)
				.HasForeignKey(gg => gg.GenreId);
		}
	}
}
