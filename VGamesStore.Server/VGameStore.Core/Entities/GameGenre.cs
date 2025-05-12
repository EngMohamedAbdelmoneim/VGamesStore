using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGameStore.Core.Entities
{
    public class GameGenre
    {
		public int GameId { get; set; }
		public virtual Game Game { get; set; }

		public int GenreId { get; set; }
		public virtual Genre Genre { get; set; }
	}
}
