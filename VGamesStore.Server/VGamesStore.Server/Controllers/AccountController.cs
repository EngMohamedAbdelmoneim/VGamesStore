using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VGameStore.Application.DTOs.IdentityDtos;
using VGameStore.Application.Interfaces.AuthInterfaces;

namespace VGamesStore.Api.Controllers
{
    public class AccountController : BaseController
    {
		private readonly IUserService _userService;

		public AccountController(IUserService userService)
		{
			_userService = userService;
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Register([FromBody] RegisterDto dto)
			=> Ok(await _userService.RegisterAsync(dto));

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			var userDto = await _userService.LoginAsync(dto);
			return Ok(userDto);
		}
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (userId == null)
				return Unauthorized();

			await _userService.LogoutAsync(userId);

			return Ok(new { message = "Logged out successfully" });
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
		=> Ok(await _userService.RefreshTokenAsync(dto.RefreshToken));

	}
}
