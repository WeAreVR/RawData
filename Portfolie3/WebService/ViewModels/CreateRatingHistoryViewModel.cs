using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServiceLib.Domain;

namespace WebService.ViewModels
{
	public class CreateRatingHistoryViewModel
	{
		public string Url { get; set; }

		public string Username { get; set; }
		public string TitleId { get; set; }
		public int Rating { get; set; }
	}
}