using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.Interfaces;
using VGameStore.Core.Entities;
using VGameStore.Core.Specifications;
using VGameStore.Infrastructure.Persistence;
 
namespace VGameStore.Application.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly GameStoreDbContext _context;

		private readonly DbSet<T> _dbSet;

		public GenericRepository(GameStoreDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>(); // Get the corresponding DbSet<T> dynamically
		}

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
		}	

		public async Task DeleteAsync(int id)
		{
			var entity = await _dbSet.FindAsync(id);
			if (entity != null)
			{
			     _dbSet.Remove(entity);
				await _context.SaveChangesAsync();

			}
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}


		public async Task<T?> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}


		public async Task UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			await _context.SaveChangesAsync();
		}
		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(BaseSpecification<T> spec)
		{
			return await ApplySpecification(spec).ToListAsync();
		}

		public async Task<T?> GetByIdWithSpecAsync(BaseSpecification<T> spec)
		{
			return await ApplySpecification(spec).FirstOrDefaultAsync();
		}
		private IQueryable<T> ApplySpecification(BaseSpecification<T> spec)
		{
			return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
		}

	}
}
