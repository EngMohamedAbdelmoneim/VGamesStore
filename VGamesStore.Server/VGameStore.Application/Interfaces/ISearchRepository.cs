using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
using VGameStore.Core.Entities;

namespace VGameStore.Application.Interfaces
{
    public interface ISearchRepository
    {
		Task<IReadOnlyList<Game>> SearchGamesAsync(SearchDto query);
		Task<IReadOnlyList<Game>> SearchGamesByCategoryAsync(string category);
		Task<IReadOnlyList<Game>> SortGamesAscendingAsync();
		Task<IReadOnlyList<Game>> SortGamesDescendingAsync();
		Task<IReadOnlyList<Game>> SortGamesByPriceAsync(int? minPrice, int? maxPrice);
		Task<IReadOnlyList<Game>> FilterGamesAsync(FilterDto query);


	}
}
