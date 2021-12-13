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
using WebService.Attributes;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/searchhistory")]
    public class SearchHistoryController : Controller
    {

        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public SearchHistoryController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }


        [HttpGet("{username}")]
        public IActionResult GetSearchHistoryByUsername(string username, [FromQuery] QueryString queryString)
        {
            var searches = _dataService.GetSearchHistoryByUsername(username, queryString);

            if (searches == null)
            {
                return NotFound();
            }

            var numberOfSearches = searches.Count();

            var items = searches.Select(GetSearchHistoryViewModel);
            var result = CreateResultModel(queryString, numberOfSearches, items);

            return Ok(result);
        }


        [HttpGet("{username}/{timestamp}")]
        public IActionResult GetSearchHistory(string username, DateTime timeStamp)
        {
            if (username == null)
            {
                return NotFound();
            }

            SearchHistory searchHistory = new SearchHistory()
            {
                SearchInput = username,
                TimeStamp = timeStamp
            };

            SearchHistoryViewModel model = GetSearchHistoryViewModel(searchHistory);

            return Ok(model);
        }

        [Authorization]
        [HttpGet]
        public IActionResult GetSearchHistory([FromQuery] QueryString queryString)
        {
            try
            {
                var user = Request.HttpContext.Items["User"] as User;
                var searches = _dataService.GetSearchHistoryByUsername(user.Username, queryString);
                Console.WriteLine(user + "hello" + user.Username + "hejsa");

               /* if (searches == null)
                {
                    return NotFound();
                }*/

                var numberOfSearches = searches.Count();

                var items = searches.Select(GetSearchHistoryViewModel);
                var result = CreateResultModel(queryString, numberOfSearches, items);

                return Ok(result);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        
        [HttpDelete("{username}")]
        public IActionResult DeleteSearchHistory(string username)
        {
           _dataService.DeleteSearchHistory(username);


            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateSearchHistory(SearchHistoryViewModel model)
        {
            var searchHistory = _mapper.Map<SearchHistory>(model);

            _dataService.CreateSearchHistory(searchHistory);

            return Created(GetUrl(searchHistory), CreateSearchHistoryViewModel(searchHistory));
        }


        private SearchHistoryViewModel GetSearchHistoryViewModel(SearchHistory searchHistory)
        {
            return new SearchHistoryViewModel
            {
                SearchInput = searchHistory.SearchInput,
                TimeStamp = searchHistory.TimeStamp

            };
        }

        private SearchHistoryViewModel GetTitleBasicViewModel(SearchHistory searchHistory)
        {
            return new SearchHistoryViewModel
            {
                SearchInput = searchHistory.SearchInput,
                TimeStamp = searchHistory.TimeStamp
            };
        }


        private SearchHistoryViewModel CreateSearchHistoryViewModel(SearchHistory searchHistory)
        {
            var model = _mapper.Map<SearchHistoryViewModel>(searchHistory);
            model.SearchInput = searchHistory.SearchInput;
            model.TimeStamp = searchHistory.TimeStamp;
            return model;
        }
        private string GetUrl(SearchHistory searchHistory)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetSearchHistory), new { searchHistory.TimeStamp });
        }

        private string GetUrl(int page, int pageSize)
        {
            return _linkGenerator.GetUriByName(
                HttpContext,
                nameof(GetSearchHistory),
                new { page, pageSize });
        }


        private object CreateResultModel(QueryString queryString, int total, IEnumerable<SearchHistoryViewModel> model)
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
            return queryString.Page >= lastPage ? null : GetUrl(queryString.Page + 1, queryString.PageSize);
        }


        private string CreateCurrentPageLink(QueryString queryString)
        {
            return GetUrl(queryString.Page, queryString.PageSize);
        }

        private string CreateNextPageLink(QueryString queryString)
        {
            return queryString.Page <= 0 ? null : GetUrl(queryString.Page - 1, queryString.PageSize);
        }

        private static int GetLastPage(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize) - 1;
        }


    }
}
