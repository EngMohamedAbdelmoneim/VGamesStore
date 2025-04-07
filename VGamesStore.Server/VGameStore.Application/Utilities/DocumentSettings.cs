using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGameStore.Application.Utilities
{
    class DocumentSettings
    {
        public async static Task<string> UploadFile(IFormFile file, string folderName)
        {
			string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files",folderName);
			Directory.CreateDirectory(uploadPath); // Ensure directory exists

			// Generate a unique filename
			string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			string filePath = Path.Combine(uploadPath, fileName);

			// Save file to local storage
			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}

			// Construct relative image URL
			string imageUrl = $"/Files/GameImage/{fileName}";

			return imageUrl;
		}
		public static void DeleteFile(string folderName, string fileName)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", $"{folderName}/", fileName);

			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
		}
	}
}
