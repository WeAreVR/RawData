using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLib.Domain
{
	public class TitleRating
	{
		
		public decimal AvgRating { get; set; }
		public int NumVotes { get; set; }

		//[ForeignKey("TitleId")]
		public string TitleId { get; set; }
		public TitleBasic TitleBasic { get; set; }
	}
}