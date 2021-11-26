using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLib.Domain
{
	public class Award
	{
		public string TitleId { get; set; }
		public string AwardName { get; set; }
		public TitleBasic TitleBasic { get; set; }
	}
}
