using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGameStore.Core.Entities
{
    public class Genre
    {
        public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public virtual ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>();
	}

}
