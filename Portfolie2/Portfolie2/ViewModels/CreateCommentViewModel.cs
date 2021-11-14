using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portfolie2.Domain;

namespace WebService.ViewModels
{
    public class CreateCommentViewModel
    {

        public string Username { get; set; }

        public string TitleId { get; set; }

        public string Content { get; set; }

        public DateTime TimeStamp { get; set; }
        public TitleBasic TitleBasic { get; set; }
        public User User { get; set; }

    }
}
