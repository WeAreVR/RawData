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
    [Route("api/titleepisode")]
    public class TitleEpisodeController : Controller
    {

        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public TitleEpisodeController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetTitleEpisode(string id)
        {
            var titleEpisode = _dataService.GetTitleEpisode(id);

            if (titleEpisode == null)
            {
                return NotFound();
            }

            TitleEpisodeViewModel model = GetTitleEpisodeViewModel(titleEpisode);

            return Ok(model);
        }
        
        [HttpGet("allepisodes/{parentTitleId}", Name = nameof(GetTitleEpisode))]
        public IActionResult GetTitleEpisodesByParentTitleId(string parentTitleId, [FromQuery] QueryString queryString)
        {
            var titleEpisodes = _dataService.GetTitleEpisodesByParentTitleId(parentTitleId, queryString);
            var allTitleEpisodes = _dataService.GetTitleEpisodesByParentTitleId(parentTitleId);

            var items = titleEpisodes.Select(GetTitleEpisodeViewModel);
            var result = CreateResultModel(queryString, _dataService.NumberOfEpisodes(allTitleEpisodes), items);


          
            return Ok(result);
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteTitleEpisode(string id)
        {
            if (!_dataService.DeleteTitleEpisode(id))
            {
                return NotFound();
            }
            _dataService.DeleteTitleEpisode(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateTitleEpisode(TitleEpisodeViewModel model)
        {
            var title = _mapper.Map<TitleEpisode>(model);

            _dataService.CreateTitleEpisode(title);

            return Created(GetTitleEpisodeUrl(title), CreateTitleEpisodeViewModel(title));
        }


        private TitleEpisodeViewModel GetTitleEpisodeViewModel(TitleEpisode titleEpisode)
        {
            var model = _mapper.Map<TitleEpisodeViewModel>(titleEpisode);
            model.Url = GetTitleEpisodeUrl(titleEpisode);
            return model;
        }
            
        


        private TitleEpisodeViewModel CreateTitleEpisodeViewModel(TitleEpisode title)
        {
            var model = _mapper.Map<TitleEpisodeViewModel>(title);
            model.Url = GetTitleEpisodeUrl(title);
            model.Id = title.Id;
            return model;
            
        }

        private string GetTitleEpisodeUrl(TitleEpisode titleEpisode)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleEpisode), new { titleEpisode.Id });

        }
        private string GetTitleEpisodeUrl(int page, int pageSize)
        {
            return _linkGenerator.GetUriByName(
                HttpContext,
                nameof(GetTitleEpisode),
                new { page, pageSize });
        }


        private object CreateResultModel(QueryString queryString, int total, IEnumerable<TitleEpisodeViewModel> model)
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
            return queryString.Page >= lastPage ? null : GetTitleEpisodeUrl(queryString.Page + 1, queryString.PageSize);
        }


        private string CreateCurrentPageLink(QueryString queryString)
        {
            return GetTitleEpisodeUrl(queryString.Page, queryString.PageSize);
        }

        private string CreateNextPageLink(QueryString queryString)
        {
            return queryString.Page <= 0 ? null : GetTitleEpisodeUrl(queryString.Page - 1, queryString.PageSize);
        }

        private static int GetLastPage(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize) - 1;
        }
    }
}
