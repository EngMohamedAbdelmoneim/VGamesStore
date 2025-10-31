using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VGameStore.Application.DTOs.IdentityDtos;
using VGameStore.Application.Interfaces.AuthInterfaces;

namespace VGamesStore.Api.Controllers
{
    public class AuthController : BaseController
    {
		private readonly IUserService _userService;

		public AuthController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromForm]RegisterDto dto)
		{
			var result = await _userService.RegisterAsync(dto);
			return Ok(result);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromForm]LoginDto dto)
		{
			var userDto = await _userService.LoginAsync(dto);
			if (userDto == null) return Unauthorized("Invalid credentials");

			return Ok(userDto);
		}
		[Authorize]
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			await _userService.LogoutAsync(userId);
			return Ok("Logged out");
		}

		[Authorize]
		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh()
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var newToken = await _userService.RefreshTokenAsync(userId);
			return Ok(new { token = newToken });
		}
	}
}
