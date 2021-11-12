using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
	public class TitleEpisode
	{
		public string Id { get; set; }
		public string ParentTitleId { get; set; }
		public int SeasonNumber { get; set; }
		public int EpisodeNumber { get; set; }
		public TitleBasic TitleBasic { get; set; }
	}
}