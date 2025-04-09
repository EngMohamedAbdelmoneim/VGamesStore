using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;
using VGameStore.Application.Services;

namespace VGamesStore.Api.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _cartegoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_cartegoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCategory() {
			var categories = await _cartegoryService.GetAllCategoriesAsync();
			return Ok(categories);
		}
		[HttpGet]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			var category = await _cartegoryService.GetCategoryByIdAsync(id);
		    if(category == null) return NotFound();
			return Ok(category);
		}
		[HttpPost]
		public async Task<IActionResult> CreateCategory([FromForm] CategoryDto category)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			
			var categoryResult =  await _cartegoryService.CreateCategoryAsync(category);
			return CreatedAtAction(nameof(GetCategoryById), new { id = categoryResult.Id }, categoryResult);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id ,[FromForm] CategoryDto categoryDto)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var result = await _cartegoryService.UpdateCategoryAsync(id, categoryDto);
			if (!result) return NotFound();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var result = await _cartegoryService.DeleteCategoryAsync(id);
			if (!result) return NotFound();
			return NoContent();
		}


	}
}
