using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
	public class NameBasics
	{
		public string Id { get; set; }
		public string PrimaryName { get; set; }
		public string BirthYear { get; set; }
		public string DeathYear { get; set; }
		public float rating { get; set; }
	}
}
