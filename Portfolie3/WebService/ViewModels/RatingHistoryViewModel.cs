using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServiceLib.Domain;

namespace WebService.ViewModels
{
	public class RatingHistoryViewModel
	{
		public string Url { get; set; }
		public string Username { get; set; }
		public string PrimaryTitle { get; set; }
		public float Rating { get; set; }
	}
}