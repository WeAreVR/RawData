using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServiceLib.Domain;

namespace WebService.ViewModels
{
    public class NameBasicViewModel
    {
		public string Url { get; set; }
        public string Id { get; set; }
		public string PrimaryName { get; set; }
		public string BirthYear { get; set; }
		public string DeathYear { get; set; }
		public float Rating { get; set; }
		public ICollection<string> ListKnownForTitles { get; set; }
		public ICollection<PlaysViewModel> ListPlays { get; set; }
		public ICollection<string> ListProfessions { get; set; }
		public ICollection<TitlePrincipalViewModel> ListTitlePrincipals { get; set; }

	}
}