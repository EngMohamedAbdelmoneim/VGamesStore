using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace VGameStore.Application.DTOs
{

	public class CreateGameDto
	{
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(18,5)")]
		public decimal Price { get; set; }
		public string Type { get; set; } = string.Empty;
		public string Developer { get; set; } = string.Empty;
		public DateTime ReleaseDate { get; set; }
		public IFormFile? File { get; set; }

	}

	public class UpdateGameDto
	{
		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		[DataType(DataType.Currency)]
		[Column(TypeName = "decimal(18,5)")]
		public decimal Price { get; set; }
		public string Type { get; set; } = string.Empty;
		public string Developer { get; set; } = string.Empty;
		public DateTime ReleaseDate { get; set; }
		public IFormFile? File { get; set; }

	}

	public class GameDto
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
	}
}

