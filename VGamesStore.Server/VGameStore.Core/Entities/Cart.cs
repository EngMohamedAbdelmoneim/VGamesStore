using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGameStore.Application.DTOs
{
	public class Cart
	{
		public string UserId { get; set; } = "";
		public List<CartItem> Items { get; set; } = new();
	}
	public class CartItem
	{
		public int GameId { get; set; }
		public string GameName { get; set; }
		public string ImageUrl { get; set; }
		public decimal Price { get; set; }
	}
}
