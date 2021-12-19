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
        [HttpGet("{id}")]
        public IActionResult GetTitleRating(string id)
        {
            var titleRating = _dataService.GetTitleRating(id);

            if (titleRating == null)
            {
                return NotFound();
            }

            TitleRatingViewModel model = GetTitleRatingViewModel(titleRating);

            return Ok(model);
        }

     
        private TitleRatingViewModel GetTitleRatingViewModel(TitleRating titleRating)
        {
            return new TitleRatingViewModel
            {

                AvgRating = titleRating.AvgRating,
                NumVotes = titleRating.NumVotes,
                TitleId = titleRating.TitleId
            };
        }
       
        private string GetUrl(TitleRating titleRating)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleRating), new { titleRating.TitleId });
        }

    }

}