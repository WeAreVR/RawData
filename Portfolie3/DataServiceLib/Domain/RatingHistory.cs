using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLib.Domain
{
	public class RatingHistory
	{
	public string Username { get; set; }
	public string TitleId { get; set; }
	public int Rating { get; set; }
	public DateTime TimeStamp  {get; set;}
	public TitleBasic TitleBasic { get; set; }
	public User User { get; set; }

	}
}
