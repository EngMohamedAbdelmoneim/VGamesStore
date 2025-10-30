using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.Interfaces;
using VGameStore.Core.Entities;
using VGameStore.Infrastructure.Persistence.GameStoreDb;

namespace VGameStore.Application.Repositories
{
	public class GameRepository : GenericRepository<Game>, IGameRepository
	{
		private readonly GameStoreDbContext _context;
		public GameRepository(GameStoreDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Game>> GetByTypeAsync(string type)
		{
			return await _context.Games.Where(g => g.Type == type).ToListAsync();
		}
		public async Task<Game> GetGameByIdAsync(int id)
		{
			var game = await _context.Games
				.Include(g => g.GameGenres)
				.ThenInclude(gg => gg.Genre)
				.AsNoTracking()
				.FirstOrDefaultAsync(g => g.Id == id);
			return game;
		}
		public async Task<IEnumerable<Game>> GetAllGamesAsync()
		{
			var games = await _context.Games
			  .Include(g => g.GameGenres)
			  .ThenInclude(gg => gg.Genre)
			  .AsNoTracking()
			  .ToListAsync();
			return games;
		}
	}
}
