using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs.IdentityDtos;
using VGameStore.Application.Interfaces.AuthInterfaces;
using VGameStore.Core.Entities.Identity;

namespace VGameStore.Application.Services.AuthServices
{
    public class UserService : IUserService
	{
        public readonly UserManager<AppUser> _userManager;
        public readonly TokenService _tokenService;
		private readonly StackExchange.Redis.IDatabase _redis;


		public UserService(UserManager<AppUser> userManager, TokenService tokenService, IConnectionMultiplexer redis)
		{
			_userManager = userManager;
			_tokenService = tokenService;
			_redis = redis.GetDatabase();

		}

		public async Task<UserDto> LoginAsync(LoginDto dto)
		{
			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
				return null;
				
			var accessToken = _tokenService.CreateTokenAsync(user,_userManager);
 			var refreshToken = _tokenService.CreateRefreshToken();
			// Save token to Redis to allow logout / blacklist
			await _redis.StringSetAsync($"token:{user.Id}",  refreshToken, TimeSpan.FromHours(1));

			return new UserDto
			{
				Id = user.Id,
				FullName = user.FullName,
				Email = user.Email,
				Token = await accessToken,
				RefreshToken = refreshToken
			};

		}

		public async Task<string> RegisterAsync(RegisterDto dto)
		{
			var userExists = await _userManager.FindByEmailAsync(dto.Email);
			if (userExists != null)
				return "User already exists";
			var appUser = new AppUser()
			{
				Email = dto.Email,
				UserName = dto.Email,
				FullName = dto.FullName,
				SecurityStamp = Guid.NewGuid().ToString()
			};
			var result = await _userManager.CreateAsync(appUser, dto.Password);
			if (!result.Succeeded)
				return "User creation failed! Please check user details and try again.";
			return "User created successfully!";
		}
		public async Task<bool> LogoutAsync(string userId)
		{
			return await _redis.KeyDeleteAsync($"refresh:{userId}");
		}

		public async Task<string> RefreshTokenAsync(string userId)
		{
			var storedToken = await _redis.StringGetAsync($"refresh:{userId}");
			if (storedToken.IsNullOrEmpty) throw new Exception("Session Expired");

			var user = await _userManager.FindByIdAsync(userId);
			var newToken = _tokenService.CreateTokenAsync(user, _userManager);
			await _redis.StringSetAsync($"refresh:{user.Id}", await newToken, TimeSpan.FromDays(7));

			return await newToken;
		}
	}
}
