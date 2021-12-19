using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataServiceLib.Domain;

namespace WebService.ViewModels
{
    public class CreateCommentViewModel
    {

        public string Username { get; set; }

        public string TitleId { get; set; }

        public string Content { get; set; }

    }
}
