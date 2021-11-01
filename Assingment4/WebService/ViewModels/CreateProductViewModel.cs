using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment4.Domain;

namespace WebService.ViewModels
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        //public Category Category { get; set; }
    }
}
