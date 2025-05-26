using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;
using VGameStore.Application.Services;
using VGameStore.Core.Entities;

namespace VGameStore.Infrastructure.RedisServices
{
    public class RedisWishlistService : IWishlistService
	{
		private readonly IDistributedCache _cache;
		private readonly JsonSerializerOptions _options;
		private readonly ILogger<RedisWishlistService> _logger;

		public RedisWishlistService(IDistributedCache cache, ILogger<RedisWishlistService> logger)
		{
			_cache = cache;
			_logger = logger;
			_options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
		}

		public async Task<Wishlist?> GetWishlistAsync(string userWishlistId)
		{
			if (string.IsNullOrWhiteSpace(userWishlistId))
				throw new ArgumentException("UserWishlistId is required.", nameof(userWishlistId));

			try
			{
				var data = await _cache.GetStringAsync(userWishlistId);
				if (string.IsNullOrEmpty(data))
				{
					// Create and store an empty wishlist if it doesn't exist
					var emptyWishlist = new Wishlist
					{
						UserWishlistId = userWishlistId,
						Items = new List<WishlistItem>()
					};

					await UpdateWishlistAsync(emptyWishlist); // Save empty wishlist
					return emptyWishlist;
				}

				return DeserializeWishlist(data);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving or creating wishlist for user: {UserWishlistId}", userWishlistId);
				return null;
			}
		}

		public async Task<Wishlist> UpdateWishlistAsync(Wishlist wishlist)
		{
			if (wishlist == null || string.IsNullOrWhiteSpace(wishlist.UserWishlistId))
				throw new ArgumentException("Wishlist or UserWishlistId cannot be null.");

			try
			{
				var data = SerializeWishlist(wishlist);
				await _cache.SetStringAsync(wishlist.UserWishlistId, data, GetCacheOptions());
				return wishlist;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error updating wishlist for user: {UserWishlistId}", wishlist.UserWishlistId);
				throw;
			}
		}

		public async Task<bool> DeleteWishlistAsync(string userWishlistId)
		{
			if (string.IsNullOrWhiteSpace(userWishlistId))
				throw new ArgumentException("UserWishlistId is required.", nameof(userWishlistId));

			try
			{
				await _cache.RemoveAsync(userWishlistId);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error deleting wishlist for user: {UserWishlistId}", userWishlistId);
				return false;
			}
		}

		public async Task<Wishlist?> RemoveItemAsync(string userWishlistId, int productId)
		{
			var wishlist = await GetWishlistAsync(userWishlistId);
			if (wishlist == null) return null;

			var item = wishlist.Items.FirstOrDefault(i => i.GameId == productId);
			if (item == null) return wishlist;

			wishlist.Items.Remove(item);
			await UpdateWishlistAsync(wishlist);

			return wishlist;
		}

		public async Task<Wishlist> AddItemAsync(string userWishlistId, WishlistItem newItem)
		{
			if (string.IsNullOrWhiteSpace(userWishlistId))
				throw new ArgumentException("UserWishlistId is required.", nameof(userWishlistId));

			if (newItem == null)
				throw new ArgumentNullException(nameof(newItem));

			var wishlist = await GetWishlistAsync(userWishlistId) ?? new Wishlist { UserWishlistId = userWishlistId, Items = new List<WishlistItem>() };

			var existingItem = wishlist.Items.FirstOrDefault(i => i.GameId == newItem.GameId);

			if (existingItem != null)
			{
				existingItem.GameId += newItem.GameId;
			}
			else
			{
				wishlist.Items.Add(newItem);
			}

			await UpdateWishlistAsync(wishlist);
			return wishlist;
		}

		// Private helper methods

		private string SerializeWishlist(Wishlist wishlist) => JsonSerializer.Serialize(wishlist, _options);

		private Wishlist? DeserializeWishlist(string? data) =>
			string.IsNullOrEmpty(data) ? null : JsonSerializer.Deserialize<Wishlist>(data, _options);

		private DistributedCacheEntryOptions GetCacheOptions() =>
			new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(7)
			};

	}
}
