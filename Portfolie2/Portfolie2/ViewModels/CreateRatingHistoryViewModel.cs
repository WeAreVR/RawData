using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolie2.Domain;

namespace WebService.ViewModels
{
	public class CreateRatingHistoryViewModel
	{
		public string Url { get; set; }

		public string Username { get; set; }
		public string TitleId { get; set; }
		public float Rating { get; set; }
	}
}