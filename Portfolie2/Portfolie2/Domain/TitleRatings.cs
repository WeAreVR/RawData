using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
	public class TitleRatings
	{
		public string Id { get; set; }
		public decimal AvgRating { get; set; }
		public int NumVotes { get; set; }
	}
}