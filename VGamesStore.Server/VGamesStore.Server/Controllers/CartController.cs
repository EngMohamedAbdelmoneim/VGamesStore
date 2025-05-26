using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;

namespace VGamesStore.Api.Controllers
{
    public class CartController : BaseController
    {
		private readonly ICartService _cartService;

		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}

		[HttpGet("{userId}")]
		public async Task<ActionResult<Cart>> GetCart(string userId)
		{
			var cart = await _cartService.GetCartAsync(userId);
			return cart ?? new Cart { UserId = userId };
		}

		[HttpPost]
		public async Task<ActionResult<Cart>> UpdateCart(Cart cart)
		{
			return await _cartService.UpdateCartAsync(cart);
		}

		[HttpDelete("{userId}")]
		public async Task<IActionResult> DeleteCart(string userId)
		{
			await _cartService.DeleteCartAsync(userId);
			return Ok();
		}
		[HttpPost("{userId}")]
		public async Task<ActionResult<Cart>> AddOrUpdateItem(string userId, CartItem item)
		{
			var cart = await _cartService.AddItemAsync(userId, item);
			return Ok(cart);
		}
		[HttpDelete("{userId}/remove/{gameId}")]
		public async Task<ActionResult<Cart>> RemoveItem(string userId, int gameId)
		{
			var cart = await _cartService.RemoveItemAsync(userId, gameId);
			return cart == null ? NotFound() : Ok(cart);
		}
	}
}
