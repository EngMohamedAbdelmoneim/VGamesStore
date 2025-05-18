using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;
using VGameStore.Core.Entities;
using static VGameStore.Core.Specifications.GameSearchCriteria;

namespace VGameStore.Application.Services
{
	public class SearchService : ISearchService
	{
		private readonly ISearchRepository _searchRepository;
		private readonly IMapper _mapper;

		public SearchService(ISearchRepository searchRepository, IMapper mapper)
		{
			_searchRepository = searchRepository;
			_mapper = mapper;

 		}

		public async Task<IReadOnlyList<GameDto>> FilterGamesAsync(FilterDto query)
		{
			var searchedGames = await _searchRepository.FilterGamesAsync(query);
			return _mapper.Map<IReadOnlyList<GameDto>>(searchedGames);
		}

		public async Task<IReadOnlyList<GameDto>> SearchGamesAsync(SearchDto query)
		{
			var searchedGames = await _searchRepository.SearchGamesAsync(query);
			return _mapper.Map<IReadOnlyList<GameDto>>(searchedGames);
		}

		public async Task<IReadOnlyList<GameDto>> SearchGamesByGenreAsync(string genre)
		{
			var searchedGames = await _searchRepository.SearchGamesByGenreAsync(genre);
			return _mapper.Map<IReadOnlyList<GameDto>>(searchedGames);
		}

		public async Task<IReadOnlyList<GameDto>> SortGamesAscendingAsync()
		{
			var sortedGames = await _searchRepository.SortGamesAscendingAsync();
			return _mapper.Map<IReadOnlyList<GameDto>>(sortedGames);
		}

		public Task<IReadOnlyList<GameDto>> SortGamesByGenreNameAsync(string genre)
		{
			throw new NotImplementedException();
		}

		public async Task<IReadOnlyList<GameDto>> SortGamesByPriceAsync(int? minPrice, int? maxPrice)
		{
			var sortedGames = await _searchRepository.SortGamesByPriceAsync(minPrice, maxPrice);
			return _mapper.Map<IReadOnlyList<GameDto>>(sortedGames);
 		}

		public async Task<IReadOnlyList<GameDto>> SortGamesDescendingAsync()
		{
 			var sortedGames = await _searchRepository.SortGamesDescendingAsync();
			return _mapper.Map<IReadOnlyList<GameDto>>(sortedGames);
		}
	}
}
