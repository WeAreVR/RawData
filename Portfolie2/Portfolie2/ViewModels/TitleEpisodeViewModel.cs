using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.ViewModels
{
    public class TitleEpisodeViewModel
    {
		public string Url { get; set; }
		public string Id { get; set; }
		public string ParentTitleId { get; set; }
		public int SeasonNumber { get; set; }
		public int EpisodeNumber { get; set; }


	}
}
