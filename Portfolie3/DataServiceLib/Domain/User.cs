using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLib.Domain
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Salt { get; set; }
		public ICollection<Bookmark> Bookmarks { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public ICollection<SearchHistory> SearchHistories { get; set; }

	}
}