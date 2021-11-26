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
    [Route("api/titlerating")]
    public class TitleRatingController : Controller
    {

        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public TitleRatingController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetTitleRating()
        {
            var titleRating = _dataService.GetTitleRating("tt10850888");

            if (titleRating == null)
            {
                return NotFound();
            }

            TitleRatingViewModel model = GetTitleRatingViewModel(titleRating);

            return Ok(model);
        }

        

        /*[HttpPost]
        public IActionResult CreateTitleRating(TitleRating model)
        {
            var rating = _mapper.Map<TitleRating>(model);

            _dataService.CreateTitleRating(rating);

            return Created(GetUrl(rating), CreateTitleRatingViewModel(rating));

        }*/

        private TitleRatingViewModel GetTitleRatingViewModel(TitleRating titleRating)
        {
            return new TitleRatingViewModel
            {

                AvgRating = titleRating.AvgRating,
                NumVotes = titleRating.NumVotes,
                TitleId = titleRating.TitleId

                //CategoryId = product.CategoryId
            };
        }
       /* private TitleRatingViewModel CreateTitleRatingViewModel(TitleRating titleRating)
        {
            var model = _mapper.Map<TitleRatingViewModel>(titleRating);
            //model.Rating = titleRating.Rating;
            return model;
        }*/
        private string GetUrl(TitleRating titleRating)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleRating), new { titleRating.TitleId });
        }

    }

}