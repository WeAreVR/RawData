using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLib.Domain
{
    public class NameBasicSearchResult
    {
        public string Id { get; set; }
        public string PrimaryName { get; set; }
        public int rank { get; set; }
    }
}
