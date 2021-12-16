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

        [HttpGet("search", Name = nameof(GetTitleBasicsSearch))]
        public IActionResult GetTitleBasicsSearch(string searchInput, [FromQuery] QueryString queryString)
        {
            var titleBasics = _dataService.GetTitleBasicsBySearch(searchInput, queryString);
            var allTitleBasics = _dataService.GetTitleBasicsBySearch(searchInput);


            if (searchInput == null)
            {
                return NotFound();
            }

            var items = titleBasics.Select(GetTitleBasicViewModel);
            var result = CreateResultModel(searchInput, queryString, _dataService.NumberOfElements(allTitleBasics), items);

            return Ok(result);
        }


        private TitleBasicViewModel GetTitleBasicViewModel(TitleBasic titleBasic)
        {
            var model = _mapper.Map<TitleBasicViewModel>(titleBasic);

           // model.Awards = titleBasic.Awards;
            model.AvgRating = titleBasic.TitleRating.AvgRating;
            //model.Genres = titleBasic.TitleGenres.Select(x => x.Genre).ToList();
            //model.TitleAkas = titleBasic.TitleAkas;
            // model.TitlePrincipals = titleBasic.TitlePrincipals;
            //model.TitleGenres = _dataService.GetTitleGenresByTitleId(titleBasic.Id);
            List<TitlePrincipalViewModel> test = new List<TitlePrincipalViewModel>();

            foreach (TitlePrincipal t in titleBasic.TitlePrincipals)
            {
                var temp = _mapper.Map<TitlePrincipalViewModel>(t);
                test.Add(temp);
            }

            model.ListTitlePrincipals = test;

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
        private string GetTitleBasicsSearchUrl(string searchInput, int page, int pageSize)
        {
            return _linkGenerator.GetUriByName(
                HttpContext,
                nameof(GetTitleBasicsSearch),
                new {searchInput, page, pageSize });
        }

        private object CreateResultModel(string searchInput, QueryString queryString, int total, IEnumerable<TitleBasicViewModel> model)
        {
            return new
            {
                total,
                prev = CreatePreviousPageLink(searchInput, queryString),
                cur = CreateCurrentPageLink(searchInput, queryString),
                next = CreateNextPageLink(searchInput, queryString, total),
                items = model
            };
        }

        private string CreateNextPageLink(string searchInput, QueryString queryString, int total)
        {
            var lastPage = GetLastPage(queryString.PageSize, total);
            return queryString.Page >= lastPage ? null : GetTitleBasicsSearchUrl(searchInput, queryString.Page + 1, queryString.PageSize);
        }


        private string CreateCurrentPageLink(string searchInput, QueryString queryString)
        {
            return GetTitleBasicsSearchUrl(searchInput, queryString.Page, queryString.PageSize);
        }

        private string CreatePreviousPageLink(string searchInput, QueryString queryString)
        {
            return queryString.Page <= 0 ? null : GetTitleBasicsSearchUrl(searchInput, queryString.Page - 1, queryString.PageSize);
        }

        private static int GetLastPage(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize) - 1;
        }
    }
}
