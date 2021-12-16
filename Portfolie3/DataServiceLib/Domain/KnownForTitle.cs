using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLib.Domain
{
	public class KnownForTitle
	{
		public string NameId { get; set; }
		public string TitleId { get; set; }
		public NameBasic NameBasic { get; set; }
		public TitleBasic TitleBasic { get; set; }

	}
}
