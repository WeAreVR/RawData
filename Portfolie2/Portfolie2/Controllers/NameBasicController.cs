using Portfolie2;
using Portfolie2.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.ViewModels;
using AutoMapper;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/namebasic")]
    public class NameBasicController : Controller
    {

        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public NameBasicController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetNameBasic(string Id)
        {
            var nameBasic = _dataService.GetNameBasic(Id);

            if (Id == null)
            {
                return NotFound();
            }

            NameBasicViewModel model = GetNameBasicViewModel(nameBasic);

            return Ok(model);
        }


        private NameBasicViewModel GetNameBasicViewModel(NameBasic nameBasic)
        {
            var model = _mapper.Map<NameBasicViewModel>(nameBasic);
            model.Id = nameBasic.Id;
            model.PrimaryName = nameBasic.PrimaryName;
            model.BirthYear = nameBasic.BirthYear;
            model.DeathYear = nameBasic.DeathYear;
            model.Rating = nameBasic.Rating;


            return model;
        }



        private string GetUrl(NameBasic nameBasic)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetNameBasic), new { nameBasic.Id });
        }
    }
}