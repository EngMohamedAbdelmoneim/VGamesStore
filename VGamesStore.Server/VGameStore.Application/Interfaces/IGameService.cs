using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
using VGameStore.Core.Entities;
using VGameStore.Core.Specifications;

namespace VGameStore.Application.Interfaces
{
    public interface IGameService
    {
			Task<IReadOnlyList<GameDto>> GetAllGamesAsync();
			Task<GameDto?> GetGameByIdAsync(int id);
			Task<bool> CreateGameAsync(CreateGameDto game);
			Task<bool> UpdateGameAsync(int id, UpdateGameDto gameDto);
			Task<bool> DeleteGameAsync(int id);

			Task<GameDtoSpec> GetByIdWithSpecAsync(BaseSpecification<Game> spec);
			Task<IReadOnlyList<GameDtoSpec>> GetAllWithSpecAsync(BaseSpecification<Game> spec);
	}
}
