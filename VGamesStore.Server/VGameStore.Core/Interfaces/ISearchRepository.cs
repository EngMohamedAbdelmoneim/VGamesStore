using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Core.Entities;
using static VGameStore.Core.Specifications.GameSearchCriteria;

namespace VGameStore.Application.Interfaces
{
    public interface ISearchRepository
    {
		Task<IReadOnlyList<Game>> SearchGamesAsync(SearchDto query);
		Task<IReadOnlyList<Game>> SearchGamesByGenreAsync(string genre);
		Task<IReadOnlyList<Game>> SortGamesAscendingAsync();
		Task<IReadOnlyList<Game>> SortGamesDescendingAsync();
		Task<IReadOnlyList<Game>> SortGamesByPriceAsync(int? minPrice, int? maxPrice);
		Task<IReadOnlyList<Game>> FilterGamesAsync(FilterDto query);
	}
}
