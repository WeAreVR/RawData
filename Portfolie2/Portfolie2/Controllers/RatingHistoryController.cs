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
    [Route("api/ratinghistory")]
    public class RatingHistoryController : Controller
    {

        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public RatingHistoryController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }
        [HttpGet("{username}/{titleId}")]
        public IActionResult GetRatingHistory(string username, string titleId)
        {
            var ratingHistory = _dataService.GetRatingHistory(username, titleId);

            if (ratingHistory == null)
            {
                return NotFound();
            }

            RatingHistoryViewModel model = GetRatingHistoryViewModel(ratingHistory);

            return Ok(model);
        }

        [HttpGet("{username}")]
        public IActionResult GetRatingHistory(string username, [FromQuery] QueryString queryString)
        {
            var ratings = _dataService.GetRatingHistoryByUsername(username, queryString);

            if (ratings == null)
            {
                return NotFound();
            }

            var numberOfRatings = ratings.Count();

            var items = ratings.Select(GetRatingHistoryViewModel);
            var result = CreateResultModel(queryString, numberOfRatings, items);

            return Ok(result);
        }

        [HttpDelete("{username}/{titleId}")]
        public IActionResult DeleteRatingHistory(string username,string titleId)
        {
            
            _dataService.DeleteRatingHistory(username, titleId);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateRatingHistory(RatingHistory model)
        {
            var rating = _mapper.Map<RatingHistory>(model);

            _dataService.CreateRatingHistory(rating);

            return Created(GetUrl(rating), CreateRatingHistoryViewModel(rating));

        }

        private RatingHistoryViewModel GetRatingHistoryViewModel(RatingHistory ratingHistory)
        {
            return new RatingHistoryViewModel
            {

                Username = ratingHistory.Username,
                TitleId = ratingHistory.TitleId,
                Rating = ratingHistory.Rating,
            };
        }
        private RatingHistoryViewModel CreateRatingHistoryViewModel(RatingHistory ratingHistory)
        {
            var model = _mapper.Map<RatingHistoryViewModel>(ratingHistory);
            model.Rating = ratingHistory.Rating;
            return model;
        }
        private string GetUrl(RatingHistory ratingHistory)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetRatingHistory), new { ratingHistory.Rating });
        }

        private string GetUrl(int page, int pageSize)
        {
            return _linkGenerator.GetUriByName(
                HttpContext,
                nameof(GetRatingHistory),
                new { page, pageSize });
        }


        private object CreateResultModel(QueryString queryString, int total, IEnumerable<RatingHistoryViewModel> model)
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