using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
	public class TitleBasics
	{
		public string Id { get; set; }
		public string TitleType { get; set; }
		public string PrimaryTitle { get; set; }
		public string OriginalTitle { get; set; }
		public bool IsAdult { get; set; }
		public string StartYear { get; set; }
		public string EndYear { get; set; }
		public int Runtime { get; set; }
		public string plot { get; set; }
		public string poster { get; set; }
	}
}