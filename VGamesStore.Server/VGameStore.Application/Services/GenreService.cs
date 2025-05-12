using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;
using VGameStore.Core.Entities;

namespace VGameStore.Application.Services
{
	public class GenreService : IGenreService
	{
		private readonly IGenericRepository<Genre> _categoryRepository;
		private readonly IMapper _mapper;
		public GenreService(IGenericRepository<Genre> categoryRepository, IMapper mapper)
		{
			_categoryRepository = categoryRepository;			
			_mapper = mapper;
		}

		public async Task<GenreDto> CreateGenreAsync(GenreDto categoryDto)
		{
				var genre = _mapper.Map<Genre>(categoryDto);
				await _categoryRepository.AddAsync(genre);
				return _mapper.Map<GenreDto>(genre);
		}

		public async Task<bool> DeleteGenreAsync(int id)
		{
			await _categoryRepository.DeleteAsync(id);
			return true;
		}

		public async Task<IReadOnlyList<GenreDto>> GetAllCategoriesAsync()
		{
		    var categories = await _categoryRepository.GetAllAsync();
			return  _mapper.Map<IReadOnlyList<GenreDto>>(categories);
		}

		public async Task<GenreDto?> GetGenreByIdAsync(int id)
		{
			var genre = await _categoryRepository.GetByIdAsync(id);
			return _mapper.Map<GenreDto>(genre);
		}

		public async Task<bool> UpdateGenreAsync(int id, GenreDto genre)
		{
			var existingGenre = await _categoryRepository.GetByIdAsync(id);
			if (existingGenre != null) {
				_mapper.Map(genre, existingGenre);
				await _categoryRepository.UpdateAsync(existingGenre);
				return true;
			}
			return false;
		}
	}
}
