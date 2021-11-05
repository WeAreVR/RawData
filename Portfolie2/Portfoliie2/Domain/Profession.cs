using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
	public class Profession
	{
		public string NameId { get; set; }
		public int Ordering { get; set; }
		public string ProfessionName { get; set; }
	}
}
