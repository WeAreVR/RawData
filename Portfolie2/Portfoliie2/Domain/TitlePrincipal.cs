using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
	public class TitlePrincipal
	{
		public string TitleId { get; set; }
		public int Ordering { get; set; }
		public string NameId { get; set; }
		public string Category { get; set; }
		public string Job { get; set; }
		public TitleBasic TitleBasic { get; set; }
		public NameBasic NameBasic { get; set; }
	}
}