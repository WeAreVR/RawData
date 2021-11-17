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
    [Route("api/titlebasic")]
    public class TitleBasicController : Controller
    {

        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public TitleBasicController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet("{titleId}")]
        public IActionResult GetTitleBasic(string titleId)
        {
            var titleBasic = _dataService.GetTitleBasic(titleId);

            if (titleId == null)
            {
                return NotFound();
            }

            TitleBasicViewModel model = GetTitleBasicViewModel(titleBasic);

            return Ok(model);
        }


        private TitleBasicViewModel GetTitleBasicViewModel(TitleBasic titleBasic)
        {
            var model = _mapper.Map<TitleBasicViewModel>(titleBasic);
            model.Awards = titleBasic.Awards;
            model.AvgRating = titleBasic.TitleRating.AvgRating;
            model.TitleGenres = titleBasic.TitleGenres;
            model.TitleAkas = titleBasic.TitleAkas;
            model.TitlePrincipals = titleBasic.TitlePrincipals;

            return model;
        }

        
        private TitleBasicViewModel CreateTitleBasicViewModel(TitleBasic title)
        {
            var model = _mapper.Map<TitleBasicViewModel>(title);
            model.Url = GetUrl(title);
            model.Id = title.Id;
           
            return model;
        }
        private string GetUrl(TitleBasic titleBasic)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleBasic), new { titleBasic.Id });
        }
    }
}
