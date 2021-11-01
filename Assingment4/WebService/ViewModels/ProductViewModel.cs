using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment4.Domain;

namespace WebService.ViewModels
{
    public class ProductViewModel
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public int CategoryId { get; set; }
        //public Category Category { get; set; }
    }
}
