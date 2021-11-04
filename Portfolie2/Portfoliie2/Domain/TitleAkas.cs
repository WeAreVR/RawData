using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
	public class TitleAkas
	{
		public string TitleId { get; set; }
		public int Ordering { get; set; }
		public string Title { get; set; }
		public string Region { get; set; }
		public string Language { get; set; }
		public string Types { get; set; }
		public string Attributes { get; set; }
		public bool IsOriginalTitle { get; set; }
	}
}
