using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.Interfaces.AuthInterfaces;
using VGameStore.Core.Entities.Identity;

namespace VGameStore.Application.Services.AuthServices
{
	public class TokenService : ITokenService		 
    {
		private readonly IConfiguration _config;
		public TokenService(IConfiguration config) => _config = config;


		public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
		{
			var claims = new List<Claim>()
			{
			new Claim(ClaimTypes.Name ,user.FullName),
			new Claim(ClaimTypes.Sid, user.Id),
			new Claim(ClaimTypes.Email, user.Email)
			};

			var userRoles = await userManager.GetRolesAsync(user);

			foreach (var role in userRoles)
				claims.Add(new Claim(ClaimTypes.Role, role));

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(_config["Jwt:Issuer"],
											 _config["Jwt:Audience"],
											 claims,
											 expires: DateTime.UtcNow.AddMinutes(30),
											 signingCredentials: creds
											 
											 );
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		public string CreateRefreshToken()
		{
			return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
		}
	}
}
