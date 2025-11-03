using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VGameStore.Application.DTOs.IdentityDtos;
using VGameStore.Application.Interfaces.AuthInterfaces;
using VGameStore.Core.Entities.Identity;

namespace VGameStore.Application.Services.AuthServices
{
    public class UserService : IUserService
	{
        public readonly UserManager<AppUser> _userManager;
        public readonly TokenService _tokenService;


		public UserService(UserManager<AppUser> userManager, TokenService tokenService)
		{
			_userManager = userManager;
			_tokenService = tokenService;
		}

		public async Task<UserDto> LoginAsync(LoginDto dto)
		{
			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
				return null;
				
			var accessToken = _tokenService.CreateTokenAsync(user,_userManager);
 			var refreshToken = _tokenService.CreateRefreshToken();
 
			return new UserDto
			{
				Id = user.Id,
				FullName = user.FullName,
				Email = user.Email,
				Token = await accessToken,
				RefreshToken = refreshToken
			};

		}

		public async Task<UserDto> RegisterAsync(RegisterDto dto)
		{
			var userExists = await _userManager.FindByEmailAsync(dto.Email);
			if (userExists != null)
				throw new Exception("User already exists");
			var appUser = new AppUser()
			{
				Email = dto.Email,
				UserName = dto.Email,
				FullName = dto.FullName,

			};
			var result = await _userManager.CreateAsync(appUser, dto.Password);
			if (!result.Succeeded)
				throw new Exception("User creation failed! Please check user details and try again.");
			return await GenerateAuthResponse(appUser);
		}

		public async Task  LogoutAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);

			if (user == null)
				return;

			// Invalidate refresh token
			user.RefreshToken = null;
			user.RefreshTokenExpiryTime = DateTime.MinValue;

			await _userManager.UpdateAsync(user);
		}

		public async Task<UserDto> RefreshTokenAsync(string refreshToken)
		{

			var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

			if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
				throw new Exception("Invalid refresh token");

			return await GenerateAuthResponse(user);
		}

		private async Task<UserDto> GenerateAuthResponse(AppUser user)
		{
			var token = await _tokenService.CreateTokenAsync(user, _userManager);
			var refreshToken = _tokenService.CreateRefreshToken();
				
			user.RefreshToken = refreshToken;
			user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

			await _userManager.UpdateAsync(user);

			return new UserDto
			{
				Id = user.Id,
				FullName = user.FullName,
				Email = user.Email,
				Token = token,
				RefreshToken = refreshToken
			};
		}
	}
}
