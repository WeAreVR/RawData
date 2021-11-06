using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
	public class SearchHistory
	{
		public string Username { get; set; }
		public string SearchInput { get; set; }
		public DateTime TimeStamp { get; set; }
		public User User { get; set; }
	}
}