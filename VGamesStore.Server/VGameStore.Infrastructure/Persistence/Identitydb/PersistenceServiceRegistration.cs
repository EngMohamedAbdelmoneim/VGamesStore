using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VGameStore.Infrastructure.Persistence.Identitydb;
namespace VGameStore.Infrastructure.Persistence.GameStoreDb
{

	public static class IdentityPersistenceServiceRegistration
	{
		public static IServiceCollection AddIdentityPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<VGameStoreIdentityDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

			return services;
		}
	}
}
