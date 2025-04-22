using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
using VGameStore.Core.Entities;

namespace VGameStore.Application.Interfaces
{
    public interface ISearchService
    {
		Task<IReadOnlyList<GameDto>> SearchGamesAsync(SearchDto? query);
		Task<IReadOnlyList<GameDto>> SearchGamesByCategoryAsync(string category);
		Task<IReadOnlyList<GameDto>> SortGamesAscendingAsync();
		Task<IReadOnlyList<GameDto>> SortGamesDescendingAsync();
		Task<IReadOnlyList<GameDto>> SortGamesByPriceAsync(int? minPrice, int? maxPrice);
		Task<IReadOnlyList<GameDto>> FilterGamesAsync(FilterDto query);

	}
}
