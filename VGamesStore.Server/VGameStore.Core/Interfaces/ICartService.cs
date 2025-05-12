using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;

namespace VGameStore.Application.Interfaces
{
    public interface ICartService
    {
			Task<Cart?> GetCartAsync(string userId);
			Task<Cart> UpdateCartAsync(Cart cart);
			Task<bool> DeleteCartAsync(string userId);
			Task<Cart> AddItemAsync(string userId, CartItem newItem);
			Task<Cart> RemoveItemAsync(string userId, int productId);
	}
}
