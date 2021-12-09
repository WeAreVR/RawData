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

        [HttpGet("search", Name = nameof(GetNameBasicsSearch))]
        public IActionResult GetNameBasicsSearch(string searchInput, [FromQuery] QueryString queryString)
        {
            var nameBasics = _dataService.GetNameBasicsBySearch(searchInput, queryString);
            var allNameBasics = _dataService.GetNameBasicsBySearch(searchInput);


            if (searchInput == null)
            {
                return NotFound();
            }

            var items = nameBasics.Select(GetNameBasicViewModel);
            var result = CreateResultModel(searchInput, queryString, _dataService.NumberOfElements(allNameBasics), items);

            return Ok(result);
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

        private string GetNameBasicsSearchUrl(string searchInput, int page, int pageSize)
        {
            return _linkGenerator.GetUriByName(
                HttpContext,
                nameof(GetNameBasicsSearch),
                new { searchInput, page, pageSize });
        }

        private object CreateResultModel(string searchInput, QueryString queryString, int total, IEnumerable<NameBasicViewModel> model)
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
            return queryString.Page >= lastPage ? null : GetNameBasicsSearchUrl(searchInput, queryString.Page + 1, queryString.PageSize);
        }


        private string CreateCurrentPageLink(string searchInput, QueryString queryString)
        {
            return GetNameBasicsSearchUrl(searchInput, queryString.Page, queryString.PageSize);
        }

        private string CreatePreviousPageLink(string searchInput, QueryString queryString)
        {
            return queryString.Page <= 0 ? null : GetNameBasicsSearchUrl(searchInput, queryString.Page - 1, queryString.PageSize);
        }

        private static int GetLastPage(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize) - 1;
        }
    }
}