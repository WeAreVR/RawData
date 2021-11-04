
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
		public string NameID { get; set; }
		public string TitleID { get; set; }
		public String Character { get; set; }
	}
}
