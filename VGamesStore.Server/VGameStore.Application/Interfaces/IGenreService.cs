using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
 
namespace VGameStore.Application.Interfaces
{
    public interface IGenreService
    {
		Task<IReadOnlyList<GenreDto>> GetAllCategoriesAsync();
		Task<GenreDto?> GetGenreByIdAsync(int id);
		Task<GenreDto> CreateGenreAsync(GenreDto genre);
		Task<bool> UpdateGenreAsync(int id, GenreDto genre);
		Task<bool> DeleteGenreAsync(int id);
	}
}
