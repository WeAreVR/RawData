
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
	public class Plays
	{
		public string NameId { get; set; }
		public string TitleId { get; set; }
		public string Character { get; set; }
	}
}
