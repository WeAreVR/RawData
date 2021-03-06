using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
	public class TitleAka
	{
		public string TitleId { get; set; }
		public int Ordering { get; set; }
		public string Title { get; set; }
		public string Region { get; set; }
		public string Language { get; set; }
		public string Type { get; set; }
		public string Attribute { get; set; }
		public bool IsOriginalTitle { get; set; }
		public TitleBasic TitleBasic { get; set; }
	}
}
