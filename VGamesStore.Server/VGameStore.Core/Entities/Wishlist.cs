using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGameStore.Core.Entities
{
    public class Wishlist
    {
		public string UserWishlistId { get; set; } = "";
		public List<WishlistItem> Items { get; set; } = new();
		
	}
	public class WishlistItem
	{
		public int GameId { get; set; }
		public string GameName { get; set; }
		public string ImageUrl { get; set; }
		public decimal Price { get; set; }
	}
}
