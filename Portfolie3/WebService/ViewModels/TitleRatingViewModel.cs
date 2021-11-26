using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServiceLib.Domain;

namespace WebService.ViewModels
{
    public class TitleRatingViewModel
    {
        public decimal AvgRating { get; set; }
        public int NumVotes { get; set; }
        public string TitleId { get; set; }

    }
}
