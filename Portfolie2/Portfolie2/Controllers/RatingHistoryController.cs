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
    [Route("api/rating")]
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

                //CategoryId = product.CategoryId
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

    }

}