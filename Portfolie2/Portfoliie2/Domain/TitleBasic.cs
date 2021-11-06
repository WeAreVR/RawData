using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
	public class TitleBasic
	{
		public string Id { get; set; }
		public string TitleType { get; set; }
		public string PrimaryTitle { get; set; }
		public string OriginalTitle { get; set; }
		public bool IsAdult { get; set; }
		public string StartYear { get; set; }
		public string EndYear { get; set; }
		public int Runtime { get; set; }
		public string Plot { get; set; }
		public string Poster { get; set; }
		public ICollection<Award> Awards { get; set; }
		public ICollection<Bookmark> Bookmarks { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public ICollection<KnownForTitle> KnownForTitles { get; set; }
		public ICollection<Plays> Plays { get; set; }
		public ICollection<RatingHistory> RatingHistories { get; set; }
		public ICollection<TitleAka> TitleAkas { get; set; }
		public ICollection<TitleEpisode> TitleEpisodes { get; set; }
		public ICollection<TitleGenre> TitleGenres { get; set; }
		public ICollection<TitlePrincipal> TitlePrincipals { get; set; }
		public TitleRating TitleRating { get; set; }
		public ICollection<Wi> Wis { get; set; }





	}
}