using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs.IdentityDtos;

namespace VGameStore.Application.Interfaces.AuthInterfaces
{
    public interface IUserService
    {
		Task<string> RegisterAsync(RegisterDto dto);
		Task<UserDto> LoginAsync(LoginDto dto);
		Task<bool> LogoutAsync(string userId);
		Task<string> RefreshTokenAsync(string userId);
	}
}
