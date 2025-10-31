using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;
using VGameStore.Core.Entities;

namespace VGamesStore.Api.Controllers
{
	[Authorize]
	public class GameController : BaseController
	{
		private readonly IGameService _gameService;

		public GameController(IGameService gameService)
		{
			_gameService = gameService;
		}
		// ✅ CREATE a new game
		[HttpPost]
		public async Task<IActionResult> CreateGame([FromForm] CreateGameDto game)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			if (game.File == null || game.File.Length == 0)
				return BadRequest("Image file is required.");
			var gameResult = await _gameService.CreateGameAsync(game);
			return Ok();
		}

		// ✅ UPDATE an existing game
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateGame(int id, [FromForm] UpdateGameDto gameDto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var result = await _gameService.UpdateGameAsync(id,gameDto);
			if (!result) return NotFound();
			return NoContent();
		}
			
		// ✅ DELETE a game
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGame(int id)
		{
			var result = await _gameService.DeleteGameAsync(id);
			if (!result) return NotFound();
			return NoContent();
		}
		[HttpGet]
		public async Task<IActionResult> GetAllGames()
		{
			var spec = new GameWithGenresAndImagesSpec();
			var games = await _gameService.GetAllWithSpecAsync(spec);
			return Ok(games);
		}	

		[HttpGet("{id}")]
		public async Task<IActionResult> GetGameById(int id)
		{
			var spec = new GameWithGenresAndImagesSpec(id);
			var game = await _gameService.GetByIdWithSpecAsync(spec);
			return game == null ? NotFound() : Ok(game);
		}
	}

}
