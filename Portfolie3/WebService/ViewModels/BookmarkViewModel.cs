using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServiceLib.Domain;

namespace WebService.ViewModels
{
    public class BookmarkViewModel
    {
        public string TitleId { get; set; }
        public string PrimaryTitle { get; set; }
        public string Url { get; set; }
    }
}