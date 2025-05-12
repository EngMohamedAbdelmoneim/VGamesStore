using AutoMapper;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;
using VGameStore.Core.Entities;
using VGameStore.Application.Mappings;
using VGameStore.Application.Utilities;
using Microsoft.AspNetCore.Http;
using VGameStore.Core.Specifications;
namespace VGameStore.Application.Services
{
	public class GameService : IGameService
	{
		private readonly IGameRepository _gameRepository;
		private readonly IMapper _mapper;

		public GameService(IGameRepository gameRepository, IMapper mapper)
		{
			_gameRepository = gameRepository;
			_mapper = mapper;
		}

		public async Task<bool> CreateGameAsync(CreateGameDto createGame)
		{
			var mappedGame = _mapper.Map<Game>(createGame);
			mappedGame.ImageUrl = await DocumentSettings.UploadFile(createGame.File, "GameImage");
			mappedGame.GameGenres = createGame.GenreIds
				.Select(id => new GameGenre { GenreId = id })
				.ToList();

			if (createGame.ImagesFiles?.Any() == true)
			{
				mappedGame.Images = new List<GameImage>();
				await HandleGameImages(createGame.ImagesFiles, mappedGame);
			}

			await _gameRepository.AddAsync(mappedGame);
			return true;
		}

		public async Task<bool> DeleteGameAsync(int id)
		{
			var existingGame = await _gameRepository.GetByIdAsync(id);
			if (existingGame == null) return false;

			if (!string.IsNullOrEmpty(existingGame.ImageUrl))
			{
				DocumentSettings.DeleteFile("GameImage", existingGame.ImageUrl);
			}

			if (existingGame.Images?.Any() == true)
			{
				foreach (var image in existingGame.Images)
				{
					DocumentSettings.DeleteFile("GameImage", image.ImageUrl);
				}
			}

			await _gameRepository.DeleteAsync(id);
			return true;
		}

		public async Task<IReadOnlyList<GameDto>> GetAllGamesAsync()
		{
			var existingGames = await _gameRepository.GetAllGamesAsync();
			return _mapper.Map<IReadOnlyList<GameDto>>(existingGames);
		}

		public async Task<GameDto?> GetGameByIdAsync(int id)
		{
			var existingGame = await _gameRepository.GetGameByIdAsync(id);
			return existingGame == null ? null : _mapper.Map<GameDto>(existingGame);
		}

		public async Task<bool> UpdateGameAsync(int id, UpdateGameDto updateGame)
		{
			var existingGame = await _gameRepository.GetByIdAsync(id);
			if (existingGame == null) return false;

			if (updateGame.File != null)
			{
				DocumentSettings.DeleteFile("GameImage", existingGame.ImageUrl);
				existingGame.ImageUrl = await DocumentSettings.UploadFile(updateGame.File, "GameImage");
			}

			if (updateGame.ImagesFiles?.Any() == true)
			{
				if (existingGame.Images != null)
				{
					foreach (var image in existingGame.Images.ToList())
					{
						DocumentSettings.DeleteFile("GameImage", image.ImageUrl);
					}
					existingGame.Images.Clear();
				}

				await HandleGameImages(updateGame.ImagesFiles, existingGame);
			}

			existingGame.GameGenres = updateGame.GenreIds.Select(id => new GameGenre { GenreId = id }).ToList();
			_mapper.Map(updateGame, existingGame);

			await _gameRepository.UpdateAsync(existingGame);
			return true;
		}

		private async Task HandleGameImages(ICollection<IFormFile> imagesFiles, Game game)
		{
			foreach (var image in imagesFiles)
			{
				var gameImage = new GameImage
				{
					ImageUrl = await DocumentSettings.UploadFile(image, "GameImage")
				};
				game.Images.Add(gameImage);
			}
		}

		public async Task<IReadOnlyList<GameDtoSpec>> GetAllWithSpecAsync(BaseSpecification<Game> spec)
		{
			var games = await _gameRepository.GetAllWithSpecAsync(spec);
			return _mapper.Map<IReadOnlyList<GameDtoSpec>>(games);
		}

		public async Task<GameDtoSpec?> GetByIdWithSpecAsync(BaseSpecification<Game> spec)
		{
			var game = await _gameRepository.GetByIdWithSpecAsync(spec);
			return game == null ? null : _mapper.Map<GameDtoSpec>(game);
		}
	}
}
