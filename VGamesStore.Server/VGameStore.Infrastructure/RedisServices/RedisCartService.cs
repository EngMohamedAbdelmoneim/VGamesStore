using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;


namespace VGameStore.Infrastructure.Services
{
	public class RedisCartService : ICartService
	{
		private readonly IDistributedCache _cache;
		private readonly JsonSerializerOptions _options;
		private readonly ILogger<RedisCartService> _logger;

		public RedisCartService(IDistributedCache cache, ILogger<RedisCartService> logger)
		{
			_cache = cache;
			_logger = logger;
			_options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
		}

		public async Task<Cart?> GetCartAsync(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
				throw new ArgumentException("UserId is required.", nameof(userId));

			try
			{
				var data = await _cache.GetStringAsync(userId);
				if (string.IsNullOrEmpty(data))
				{
					// Create and store an empty cart if it doesn't exist
					var emptyCart = new Cart
					{
						UserId = userId,
						Items = new List<CartItem>()
					};

					await UpdateCartAsync(emptyCart); // Save empty cart
					return emptyCart;
				}

				return DeserializeCart(data);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving or creating cart for user: {UserId}", userId);
				return null;
			}
		}

		public async Task<Cart> UpdateCartAsync(Cart cart)
		{
			if (cart == null || string.IsNullOrWhiteSpace(cart.UserId))
				throw new ArgumentException("Cart or UserId cannot be null.");

			try
			{
				var data = SerializeCart(cart);
				await _cache.SetStringAsync(cart.UserId, data, GetCacheOptions());
				return cart;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error updating cart for user: {UserId}", cart.UserId);
				throw;
			}
		}

		public async Task<bool> DeleteCartAsync(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
				throw new ArgumentException("UserId is required.", nameof(userId));

			try
			{
				await _cache.RemoveAsync(userId);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error deleting cart for user: {UserId}", userId);
				return false;
			}
		}

		public async Task<Cart?> RemoveItemAsync(string userId, int productId)
		{
			var cart = await GetCartAsync(userId);
			if (cart == null) return null;

			var item = cart.Items.FirstOrDefault(i => i.GameId == productId);
			if (item == null) return cart;

			cart.Items.Remove(item);
			await UpdateCartAsync(cart);

			return cart;
		}

		public async Task<Cart> AddItemAsync(string userId, CartItem newItem)
		{
			if (string.IsNullOrWhiteSpace(userId))
				throw new ArgumentException("UserId is required.", nameof(userId));

			if (newItem == null)
				throw new ArgumentNullException(nameof(newItem));

			var cart = await GetCartAsync(userId) ?? new Cart { UserId = userId, Items = new List<CartItem>() };

			var existingItem = cart.Items.FirstOrDefault(i => i.GameId == newItem.GameId);

			if (existingItem != null)
			{
				existingItem.GameId += newItem.GameId;
			}
			else
			{
				cart.Items.Add(newItem);
			}

			await UpdateCartAsync(cart);
			return cart;
		}

		// Private helper methods

		private string SerializeCart(Cart cart) => JsonSerializer.Serialize(cart, _options);

		private Cart? DeserializeCart(string? data) =>
			string.IsNullOrEmpty(data) ? null : JsonSerializer.Deserialize<Cart>(data, _options);

		private DistributedCacheEntryOptions GetCacheOptions() =>
			new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
			};
	}
}
