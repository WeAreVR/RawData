using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolie2.Domain
{
    public class Comment
    {
        public string Username { get; set; }

        public string TitleId { get; set; }

        public string Content { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}