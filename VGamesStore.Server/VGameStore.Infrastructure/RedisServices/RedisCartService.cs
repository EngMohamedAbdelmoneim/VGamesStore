using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
using VGameStore.Application.Interfaces;

namespace VGameStore.Application.Services
{
    public class RedisCartService : ICartService
    {
		private readonly IDistributedCache _cache;
		private readonly JsonSerializerOptions _options;

		public RedisCartService(IDistributedCache cache)
		{
			_cache = cache;
			_options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
		}
			
		public async Task<Cart?> GetCartAsync(string userId)
		{
			var data = await _cache.GetStringAsync(userId);
			return data == null ? null : JsonSerializer.Deserialize<Cart>(data, _options);
		}

		public async Task<Cart> UpdateCartAsync(Cart cart)
		{
			var data = JsonSerializer.Serialize(cart, _options);
			await _cache.SetStringAsync(cart.UserId, data);
			return cart;
		}

		public async Task<bool> DeleteCartAsync(string userId)
		{
			await _cache.RemoveAsync(userId);
			return true;
		}
		public async Task<Cart?> RemoveItemAsync(string userId, int productId)
		{
			var cart = await GetCartAsync(userId);
			if (cart == null) return null;

			var item = cart.Items.FirstOrDefault(i => i.GameId == productId);
			if (item == null) return cart;

			cart.Items.Remove(item);

			var updatedData = JsonSerializer.Serialize(cart, _options);
			await _cache.SetStringAsync(userId, updatedData);

			return cart;
		}

		public async Task<Cart> AddItemAsync(string userId, CartItem newItem)
		{
			var cart = await GetCartAsync(userId) ?? new Cart { UserId = userId, Items = new List<CartItem>() };

			var existingItem = cart.Items.FirstOrDefault(i => i.GameId == newItem.GameId);

			cart.Items.Add(newItem);

			await UpdateCartAsync(cart);
			return cart;
		}

	}
}
