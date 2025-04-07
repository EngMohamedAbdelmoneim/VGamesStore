using AutoMapper;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;
using VGameStore.Core.Entities;
using VGameStore.Application.Mappings;
using VGameStore.Application.Utilities;
namespace VGameStore.Application.Services
{
	public class GameService : IGameService
	{
		private readonly IGenericRepository<Game> _gameRepository;
		private readonly IMapper _mapper;

		public GameService(IGenericRepository<Game> gameRepository, IMapper mapper )
		{
			_gameRepository = gameRepository;
			_mapper = mapper;
		}
		public async Task<GameDto> CreateGameAsync(CreateGameDto createGame)
		{
 			var game = _mapper.Map<Game>(createGame);
			game.ImageUrl = await DocumentSettings.UploadFile(createGame.File, "GameImage");
			await _gameRepository.AddAsync(game);
			return _mapper.Map<GameDto>(game);
		}

		public async Task<bool> DeleteGameAsync(int id)
		{
			var game = await _gameRepository.GetByIdAsync(id);
			if (game == null) return false;
			if (game.ImageUrl != null)
			{
				DocumentSettings.DeleteFile("GameImage", game.ImageUrl);
			}
			await _gameRepository.DeleteAsync(id);
			return true;
		}

		public async Task<IReadOnlyList<GameDto>> GetAllGamesAsync()
		{
			var games = await _gameRepository.GetAllAsync();
			var mappedGames = _mapper.Map<IReadOnlyList<GameDto>>(games);
			return mappedGames;
		}

		public async Task<GameDto?> GetGameByIdAsync(int id)
		{
			var game = await _gameRepository.GetByIdAsync(id);
			return game == null ? null : _mapper.Map<GameDto>(game);
		}

		public	async Task<bool> UpdateGameAsync(int id, UpdateGameDto gameDto)
		{
			var existingGame = await _gameRepository.GetByIdAsync(id);
			if (existingGame == null) return false;

			if (gameDto.File != null)
			{
				DocumentSettings.DeleteFile("GameImage", existingGame.ImageUrl);
				existingGame.ImageUrl = await DocumentSettings.UploadFile(gameDto.File, "GameImage");
			}
			_mapper.Map(gameDto, existingGame);
			await _gameRepository.UpdateAsync(existingGame);
			return true;
		}
	}
}
