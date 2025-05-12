using Microsoft.AspNetCore.Mvc;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;
using VGameStore.Application.Services;
using static VGameStore.Core.Specifications.GameSearchCriteria;

namespace VGamesStore.Api.Controllers
{
    public class SearchController : BaseController
    {
		private readonly ISearchService _searchService;

		public SearchController(ISearchService searchService)
		{
			_searchService = searchService;
		}

		[HttpGet("search")]
		public async Task<IActionResult> SearchGames([FromQuery] SearchDto query)
		{
			var games = await _searchService.SearchGamesAsync(query);
			return Ok(games);
		}
		[HttpGet("search/genre")]
		public async Task<IActionResult> SearchGamesByGenre([FromQuery] string genre)
		{
			var games = await _searchService.SearchGamesByGenreAsync(genre);
			return Ok(games);
		}
		[HttpGet("sort/ascending")]
		public async Task<IActionResult> SortGamesAscending()
		{
			var games = await _searchService.SortGamesAscendingAsync();
			return Ok(games);
		}
		[HttpGet("sort/descending")]
		public async Task<IActionResult> SortGamesDescending()
		{
			var games = await _searchService.SortGamesDescendingAsync();
			return Ok(games);
		}
		[HttpGet("sort/price")]
		public async Task<IActionResult> SortGamesByPrice([FromQuery] int? minPrice, [FromQuery] int? maxPrice)
		{
			var games = await _searchService.SortGamesByPriceAsync(minPrice, maxPrice);
			return Ok(games);
		}
		[HttpGet("search")]
		public async Task<IActionResult> FilterGames([FromQuery] FilterDto query)
		{
			var games = await _searchService.FilterGamesAsync(query);
			return Ok(games);
		}
	}
}
