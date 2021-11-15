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

        [HttpGet("getsearchhistory/{input}")]
        public IActionResult GetSearchHistory(string input, DateTime timeStamp)
        {
            if (input == null)
            {
                return NotFound();
            }
            SearchHistory searchHistory = new SearchHistory()
            {
                SearchInput = input,
                TimeStamp = timeStamp
            };

            SearchHistoryViewModel model = GetSearchHistoryViewModel(searchHistory);

            return Ok(model);
        }

        private SearchHistoryViewModel GetSearchHistoryViewModel(SearchHistory searchHistory)
        {
            return new SearchHistoryViewModel
            {
                SearchInput = searchHistory.SearchInput,
                TimeStamp = searchHistory.TimeStamp

            };
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSearchHistory(string id, DateTime date)
        {
            /*
            if (!_dataService.DeleteTitleBasic(id))
            {
                return NotFound();
            }
            */
            
            _dataService.DeleteSearchHistory(id, date);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateSearchHistory(SearchHistoryViewModel model)
        {
            var searchHistory = _mapper.Map<SearchHistory>(model);

            _dataService.CreateSearchHistory(searchHistory);

            return Created(GetUrl(searchHistory), CreateSearchHistoryViewModel(searchHistory));
        }


        private SearchHistoryViewModel GetTitleBasicViewModel(Portfolie2.Domain.SearchHistory searchHistory)
        {
            return new SearchHistoryViewModel
            {

                //Url = GetUrl(TitleBasicViewModel),
                TimeStamp = searchHistory.TimeStamp,
                //CategoryId = product.CategoryId
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



    }
}
