using DataServiceLib;
using DataServiceLib.Domain;
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

        [HttpGet("search/{searchInput}")]
        public IActionResult GetTitleBasicInput(string searchInput, [FromQuery] QueryString queryString)
        {
            var titleBasics = _dataService.GetTitleBasicsBySearch(searchInput, queryString);


            if (searchInput == null)
            {
                return NotFound();
            }

            var items = titleBasics.Select(GetTitleBasicViewModel);
            var result = CreateResultModel(queryString, 10, items);
            //TitleBasicViewModel model = GetTitleBasicViewModel(titleBasic);

            return Ok(result);
        }


        private TitleBasicViewModel GetTitleBasicViewModel(TitleBasic titleBasic)
        {
            var model = _mapper.Map<TitleBasicViewModel>(titleBasic);

            //model.Awards = titleBasic.Awards;
            //model.AvgRating = titleBasic.TitleRating.AvgRating;
            //model.TitleGenres = titleBasic.TitleGenres;
            //model.TitleAkas = titleBasic.TitleAkas;
            //model.TitlePrincipals = titleBasic.TitlePrincipals;

            return model;
        }

        
        private TitleBasicViewModel CreateeTitleBasicViewModel(TitleBasic title)
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
        private string GetTitleBasicUrl(int page, int pageSize)
        {
            return _linkGenerator.GetUriByName(
                HttpContext,
                nameof(GetTitleBasic),
                new { page, pageSize });
        }

        private object CreateResultModel(QueryString queryString, int total, IEnumerable<TitleBasicViewModel> model)
        {
            return new
            {
                total,
                prev = CreateNextPageLink(queryString),
                cur = CreateCurrentPageLink(queryString),
                next = CreateNextPageLink(queryString, total),
                items = model
            };
        }
        private string CreateNextPageLink(QueryString queryString, int total)
        {
            var lastPage = GetLastPage(queryString.PageSize, total);
            return queryString.Page >= lastPage ? null : GetTitleBasicUrl(queryString.Page + 1, queryString.PageSize);
        }


        private string CreateCurrentPageLink(QueryString queryString)
        {
            return GetTitleBasicUrl(queryString.Page, queryString.PageSize);
        }

        private string CreateNextPageLink(QueryString queryString)
        {
            return queryString.Page <= 0 ? null : GetTitleBasicUrl(queryString.Page - 1, queryString.PageSize);
        }

        private static int GetLastPage(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize) - 1;
        }
    }
}
