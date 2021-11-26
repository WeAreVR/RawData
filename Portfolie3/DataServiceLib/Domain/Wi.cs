using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLib.Domain
{
	public class Wi
	{
		public int TitleId { get; set; }
		public string Word { get; set; }
		public string Field { get; set; }
		public string Lexeme { get; set; }
		public TitleBasic TitleBasic { get; set; }
	}
}

