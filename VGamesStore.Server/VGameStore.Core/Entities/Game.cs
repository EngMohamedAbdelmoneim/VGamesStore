using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGameStore.Core.Entities
{
    public class Game
    {
        public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(18,5)")]
		public decimal Price { get; set; }
		public string Type { get; set; } = string.Empty;
		public string Developer { get; set; } = string.Empty;
		public DateTime ReleaseDate { get; set; }
		public string ImageUrl { get; set; } = string.Empty;
		public virtual ICollection<GameImage> Images { get; set; } = new List<GameImage>();
		public virtual ICollection<GameGenre> GameGenres { get; set; } = new List<GameGenre>(); 

	}
}
