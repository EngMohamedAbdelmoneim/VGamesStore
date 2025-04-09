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
	public class CategoryService : ICategoryService
	{
		private readonly IGenericRepository<Category> _categoryRepository;
		private readonly IMapper _mapper;
		public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
		{
			_categoryRepository = categoryRepository;			
			_mapper = mapper;
		}

		public async Task<CategoryDto> CreateCategoryAsync(CategoryDto categoryDto)
		{
				var category = _mapper.Map<Category>(categoryDto);
				await _categoryRepository.AddAsync(category);
				return _mapper.Map<CategoryDto>(category);
		}

		public async Task<bool> DeleteCategoryAsync(int id)
		{
			await _categoryRepository.DeleteAsync(id);
			return true;
		}

		public async Task<IReadOnlyList<CategoryDto>> GetAllCategoriesAsync()
		{
		    var categories = await _categoryRepository.GetAllAsync();
			return  _mapper.Map<IReadOnlyList<CategoryDto>>(categories);
		}

		public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
		{
			var category = await _categoryRepository.GetByIdAsync(id);
			return _mapper.Map<CategoryDto>(category);
		}

		public async Task<bool> UpdateCategoryAsync(int id, CategoryDto category)
		{
			var existingCategory = await _categoryRepository.GetByIdAsync(id);
			if (existingCategory != null) {
				_mapper.Map(category, existingCategory);
				await _categoryRepository.UpdateAsync(existingCategory);
				return true;
			}
			return false;
		}
	}
}
