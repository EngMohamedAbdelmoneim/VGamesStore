using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
using VGameStore.Core.Entities;

namespace VGameStore.Application.Interfaces
{
	public interface IWishlistService
    {
		Task<Wishlist?> GetWishlistAsync(string userId);
		Task<Wishlist> UpdateWishlistAsync(Wishlist Wishlist);
		Task<bool> DeleteWishlistAsync(string userId);
		Task<Wishlist> AddItemAsync(string userId, WishlistItem newItem);
		Task<Wishlist> RemoveItemAsync(string userId, int productId);
	}
}
