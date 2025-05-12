using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGameStore.Core.Entities
{
	public class GameImage
	{
		public int Id { get; set; }	
		public string ImageUrl { get; set; } = null!;

		public int GameId { get; set; }
		public virtual Game Game { get; set; } = null!;
	}
}
