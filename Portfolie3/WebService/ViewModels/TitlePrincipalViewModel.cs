using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServiceLib.Domain;

namespace WebService.ViewModels
{
    public class TitlePrincipalViewModel
    {
        public string TitleId { get; set; }
        public string NameId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Job { get; set; }
    }
}