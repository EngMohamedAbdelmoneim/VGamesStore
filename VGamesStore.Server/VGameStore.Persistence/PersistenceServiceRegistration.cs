using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VGameStore.Application.Interfaces;
using VGameStore.Application.Repositories;
namespace VGameStore.Persistence
{

	public static class PersistenceServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<GameStoreDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			// Register Generic Repository
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			// Register Specific Repository
			services.AddScoped<IGameRepository, GameRepository>();
			return services;
		}
	}
}
