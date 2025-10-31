using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Core.Entities.Identity;

namespace VGameStore.Application.Interfaces.AuthInterfaces
{
    public interface ITokenService
    {
		Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
	}
}
