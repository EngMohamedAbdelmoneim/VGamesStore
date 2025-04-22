using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;
using VGameStore.Core.Entities;
using VGameStore.Infrastructure.Persistence;
 using VGameStore.Persistence.Migrations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VGameStore.Application.Repositories
{

    public class SearchRepository : ISearchRepository
	{
		private readonly GameStoreDbContext _context;
		public SearchRepository(GameStoreDbContext context)
		{
			_context = context;
		}

		// Search for games by keyword, category, and developer
		public async Task<IReadOnlyList<Game>> SearchGamesAsync(SearchDto query)
		{
			var games = _context.Games.AsQueryable();

			if (!string.IsNullOrEmpty(query.Keyword)) 
			{
				games = games.Where(g =>
					g.Title.Contains(query.Keyword) ||
				g.Description.Contains(query.Keyword) ||
					g.Developer.Contains(query.Keyword) ||
					g.Category.Name.Contains(query.Keyword));
			}

			return await games.ToListAsync();
		}
		public async Task<IReadOnlyList<Game>> SearchGamesByCategoryAsync(string category)
		{
			var games = _context.Games.AsQueryable();

			if (!string.IsNullOrEmpty(category))
			{
				games = games.Where(g => g.Category.Name == category);
			}
			return await games.ToListAsync();
		}
		// Sort games
		public async Task<IReadOnlyList<Game>> SortGamesAscendingAsync()
		{
			return await _context.Games
				.OrderBy(g => g.Title)
				.ToListAsync();
		}
		public async Task<IReadOnlyList<Game>> SortGamesDescendingAsync()
		{
			return await _context.Games
				  .OrderByDescending(g => g.Title)
				  .ToListAsync();
		}
		public async Task<IReadOnlyList<Game>> SortGamesByPriceAsync(int? minPrice, int? maxPrice)
		{
			var query = _context.Games.AsQueryable();

			if (minPrice.HasValue)
			{
				query = query.Where(g => g.Price >= minPrice.Value);
			}

			if (maxPrice.HasValue)
			{
				query = query.Where(g => g.Price <= maxPrice.Value);
			}

			return await query.OrderBy(g => g.Price).ToListAsync();
		}

		public async Task<IReadOnlyList<Game>> FilterGamesAsync(FilterDto query)
		{
			var games = _context.Games.AsQueryable();

			if (!string.IsNullOrEmpty(query.Keyword))
			{
				games = games.Where(g =>
					g.Title.Contains(query.Keyword) ||
					g.Description.Contains(query.Keyword) ||
					g.Developer.Contains(query.Keyword));
			}

			if (!string.IsNullOrEmpty(query.CategoryId.ToString()))
			{
				games = games.Where(g => g.CategoryId == query.CategoryId);
			}

			if (!string.IsNullOrEmpty(query.Developer))
			{
				games = games.Where(g => g.Developer == query.Developer);
			}
			//Price 
			if (query.MinPrice.HasValue)
			{
				games = games.Where(g => g.Price >= query.MinPrice.Value);
			}
			if (query.MaxPrice.HasValue)
			{
				games = games.Where(g => g.Price <= query.MaxPrice.Value);
			}
			// Sorting
			games = query.SortBy.ToLower() switch
			{
				"title" => query.Ascending ? games.OrderBy(g => g.Title) : games.OrderByDescending(g => g.Title),
				"price" => query.Ascending ? games.OrderBy(g => g.Price) : games.OrderByDescending(g => g.Price),
				"releaseDate" => query.Ascending ? games.OrderBy(g => g.ReleaseDate) : games.OrderByDescending(g => g.ReleaseDate),
				_ => games
			};

			return await games.ToListAsync();
		}
	}
}
