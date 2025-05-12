using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;
using VGameStore.Application.Services;

namespace VGamesStore.Api.Controllers
{
    public class GenreController : BaseController
    {
        private readonly IGenreService _cartegoryService;

		public GenreController(IGenreService categoryService)
		{
			_cartegoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllGenre() {
			var categories = await _cartegoryService.GetAllCategoriesAsync();
			return Ok(categories);
		}
		[HttpGet]
		public async Task<IActionResult> GetGenreById(int id)
		{
			var genre = await _cartegoryService.GetGenreByIdAsync(id);
		    if(genre == null) return NotFound();
			return Ok(genre);
		}
		[HttpPost]
		public async Task<IActionResult> CreateGenre([FromForm] GenreDto genre)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			
			var categoryResult =  await _cartegoryService.CreateGenreAsync(genre);
			return CreatedAtAction(nameof(GetGenreById), new { id = categoryResult.Id }, categoryResult);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateGenre(int id ,[FromForm] GenreDto categoryDto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var result = await _cartegoryService.UpdateGenreAsync(id, categoryDto);
			if (!result) return NotFound();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGenre(int id)
		{
			var result = await _cartegoryService.DeleteGenreAsync(id);
			if (!result) return NotFound();
			return NoContent();
		}


	}
}
