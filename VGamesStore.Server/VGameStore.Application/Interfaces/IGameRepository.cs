using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Core.Entities;

namespace VGameStore.Application.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
		Task<IEnumerable<Game>> GetByTypeAsync(string type);
	}
}
