using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLib.Domain
{
	public class Profession
	{
		public string NameId { get; set; }
		public int Ordering { get; set; }
		public string ProfessionName { get; set; }
		public NameBasic NameBasic { get; set; }
	}
}
