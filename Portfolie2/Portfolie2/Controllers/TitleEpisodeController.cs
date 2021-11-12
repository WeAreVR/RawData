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
        
        [HttpGet]
        public IActionResult GetTitleEpisode()
        {
            var titleEpisode = _dataService.GetTitleEpisode("tt10850888");

            if (titleEpisode == null)
            {
                return NotFound();
            }

            TitleEpisodeViewModel model = GetTitleEpisodeViewModel(titleEpisode);

            return Ok(model);
        }
        
        [HttpGet("gettitleepisodepage/{id}/{page}", Name = nameof(GetTitleEpisode))]
        public IActionResult GetTitleEpisodesByParentTitleId(string id, int page)
        {
            var titleEpisodes = _dataService.GetTitleEpisodesByParentTitleId(id, page, 4);

            //TitleEpisodeViewModel model = GetTitleEpisodeViewModel(titleEpisode);
            var model = titleEpisodes.Select(GetTitleEpisodeViewModel);

            return Ok(model);
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteTitleEpisode(string id)
        {
            /*if (!_dataService.DeleteTitleEpisode(id))
            {
                return NotFound();
            }*/
            _dataService.DeleteTitleEpisode(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateTitleEpisode(TitleEpisodeViewModel model)
        {
            var title = _mapper.Map<TitleEpisode>(model);

            _dataService.CreateTitleEpisode(title);

            return Created(GetUrl(title), CreateTitleEpisodeViewModel(title));
        }


        private TitleEpisodeViewModel GetTitleEpisodeViewModel(TitleEpisode titleEpisode)
        {
            return new TitleEpisodeViewModel
            {

                //Url = GetUrl(TitleEpisodeViewModel),
                ParentTitleId = titleEpisode.Id
                //CategoryId = product.CategoryId
            };
        }


        private TitleEpisodeViewModel CreateTitleEpisodeViewModel(TitleEpisode title)
        {
            var model = _mapper.Map<TitleEpisodeViewModel>(title);
            model.Url = GetUrl(title);
            model.Id = title.Id;
            return model;
        }

        private string GetUrl(TitleEpisode titleEpisode)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleEpisode), new { titleEpisode.Id });
        }



    }
}
