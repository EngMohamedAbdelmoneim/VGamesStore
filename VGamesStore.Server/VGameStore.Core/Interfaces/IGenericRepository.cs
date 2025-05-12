using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Core.Entities;
 using VGameStore.Core.Specifications;

namespace VGameStore.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
		Task<IEnumerable<T>> GetAllAsync();
		Task<T?> GetByIdAsync(int id);
		Task AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);


		Task<T> GetByIdWithSpecAsync(BaseSpecification<T> spec);
		Task<IReadOnlyList<T>> GetAllWithSpecAsync(BaseSpecification<T> spec);

		//// Optional for pagination or count
		//Task<int> CountAsync(BaseSpecification<T> spec);
	
}
}
