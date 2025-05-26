using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;
using VGameStore.Core.Entities;

namespace VGamesStore.Api.Controllers
{
    public class WishlistController : BaseController
    {
		private readonly IWishlistService _wishlistService;

		public WishlistController(IWishlistService wishlistService)
		{
			_wishlistService = wishlistService;
		}

		[HttpGet("{userWishlistId}")]
		public async Task<ActionResult<Wishlist>> GetWishlist(string userWishlistId)
		{
			var wishlist = await _wishlistService.GetWishlistAsync(userWishlistId);
			return wishlist ?? new Wishlist { UserWishlistId = userWishlistId };
		}

		[HttpPost]
		public async Task<ActionResult<Wishlist>> UpdateWishlist(Wishlist wishlist)
		{
			return await _wishlistService.UpdateWishlistAsync(wishlist);
		}

		[HttpDelete("{userWishlistId}")]
		public async Task<IActionResult> DeleteWishlist(string userWishlistId)
		{
			await _wishlistService.DeleteWishlistAsync(userWishlistId);
			return Ok();
		}
		[HttpPost("{userWishlistId}")]
		public async Task<ActionResult<Wishlist>> AddOrUpdateItem(string userWishlistId, WishlistItem item)
		{
			var wishlist = await _wishlistService.AddItemAsync(userWishlistId, item);
			return Ok(wishlist);
		}
		[HttpDelete("{userWishlistId}/remove/{gameId}")]
		public async Task<ActionResult<Wishlist>> RemoveItem(string userWishlistId, int gameId)
		{
			var wishlist = await _wishlistService.RemoveItemAsync(userWishlistId, gameId);
			return wishlist == null ? NotFound() : Ok(wishlist);
		}
	}
}
