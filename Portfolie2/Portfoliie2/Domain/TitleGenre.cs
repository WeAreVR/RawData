using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
    public class TitleGenre
    {
        public int TitleId { get; set; }
        public string Genre { get; set; }
    }
}