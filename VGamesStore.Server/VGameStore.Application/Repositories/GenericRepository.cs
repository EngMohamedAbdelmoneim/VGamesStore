using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.Interfaces;
using VGameStore.Core.Entities;
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
	}
}
