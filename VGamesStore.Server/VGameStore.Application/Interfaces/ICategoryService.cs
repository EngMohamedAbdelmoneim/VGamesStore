using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
 
namespace VGameStore.Application.Interfaces
{
    public interface ICategoryService
    {
		Task<IReadOnlyList<CategoryDto>> GetAllCategoriesAsync();
		Task<CategoryDto?> GetCategoryByIdAsync(int id);
		Task<CategoryDto> CreateCategoryAsync(CategoryDto category);
		Task<bool> UpdateCategoryAsync(int id, CategoryDto category);
		Task<bool> DeleteCategoryAsync(int id);
	}
}
