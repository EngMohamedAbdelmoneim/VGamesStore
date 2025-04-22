using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGameStore.Application.DTOs
{
    public class SearchDto
    {
		public string? Keyword { get; set; }

	}
	public class FilterDto
	{
		public string? Keyword { get; set; }
		public int? CategoryId { get; set; }
		public decimal? MinPrice { get; set; }
		public decimal? MaxPrice { get; set; }
		public string? Developer { get; set; }
		public string? SortBy { get; set; } = "title"; // or "releaseDate", "price"
		public bool Ascending { get; set; } = true;
	}
}
