using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
	public class TitlePrincipals
	{
		public string TitleId { get; set; }
		public int Ordering { get; set; }
		public string NameId { get; set; }
		public string Category { get; set; }
		public string Job { get; set; }
	}
}