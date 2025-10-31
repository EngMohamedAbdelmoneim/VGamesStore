using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Core.Entities.Identity;

namespace VGameStore.Infrastructure.Persistence.Identitydb
{

	public class VGameStoreIdentityDbContext : IdentityDbContext<AppUser>
	{
		public VGameStoreIdentityDbContext(DbContextOptions<VGameStoreIdentityDbContext> options)
			: base(options)
		{
		}
		public DbSet<RefreshToken> RefreshTokens { get; set; }

	}
}
