using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGameStore.Core.Entities.Identity
{
	public class RefreshToken
	{
		public int Id { get; set; }
		public string Token { get; set; }
		public string UserId { get; set; }
		public AppUser User { get; set; }
		public DateTime ExpiresAt { get; set; }
		public bool IsRevoked { get; set; }
	}
}
