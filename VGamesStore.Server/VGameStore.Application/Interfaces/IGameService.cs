using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
using VGameStore.Core.Entities;

namespace VGameStore.Application.Interfaces
{
    public interface IGameService
    {
			Task<IEnumerable<GameDto>> GetAllGamesAsync();
			Task<GameDto?> GetGameByIdAsync(int id);
			Task<GameDto> CreateGameAsync(CreateGameDto game);
			Task<bool> UpdateGameAsync(int id, UpdateGameDto gameDto);
			Task<bool> DeleteGameAsync(int id);

	}
}
