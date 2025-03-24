using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;
using VGameStore.Core.Entities;

namespace VGamesStore.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GameController : ControllerBase
	{
		private readonly IGameService _gameService;

		public GameController(IGameService gameService)
		{
			_gameService = gameService;
		}

		// ✅ GET all games
		[HttpGet]
		public async Task<IActionResult> GetAllGames()
		{
			var games = await _gameService.GetAllGamesAsync();
			return Ok(games);
		}

		// ✅ GET game by ID
		[HttpGet("{id}")]
		public async Task<IActionResult> GetGameById(int id)
		{
			var game = await _gameService.GetGameByIdAsync(id);
			if (game == null) return NotFound();
			return Ok(game);
		}

		// ✅ CREATE a new game
		[HttpPost]
		public async Task<IActionResult> CreateGame([FromForm] CreateGameDto game)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			if (game.File == null || game.File.Length == 0)
				return BadRequest("Image file is required.");
			var gameResult = await _gameService.CreateGameAsync(game);
			return CreatedAtAction(nameof(GetGameById), new { id = gameResult.Id }, gameResult);
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
	}

}
