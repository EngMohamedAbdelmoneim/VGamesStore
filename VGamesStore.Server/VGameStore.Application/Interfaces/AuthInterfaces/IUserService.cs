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
		Task<UserDto> RegisterAsync(RegisterDto model);
		Task<UserDto> LoginAsync(LoginDto model);
		Task<UserDto> RefreshTokenAsync(string refreshToken);
		Task LogoutAsync(string userId);
	}
}
