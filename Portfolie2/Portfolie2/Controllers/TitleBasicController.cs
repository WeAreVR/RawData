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
    [Route("api/titlebasic")]
    public class TitleBasicController : Controller
    {

        private readonly IDataService _dataService;
        private readonly LinkGenerator  _linkGenerator;
        private readonly IMapper _mapper;

        public TitleBasicController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTitleBasic()
        {
            var titleBasic = _dataService.GetTitleBasic("tt10850888");

            if (titleBasic == null)
            {
                return NotFound();
            }

            TitleBasicViewModel model = GetTitleBasicViewModel(titleBasic);

            return Ok(model);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteTitleBasic(string id)
        {
            /*if (!_dataService.DeleteTitleBasic(id))
            {
                return NotFound();
            }*/
            _dataService.DeleteTitleBasic(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateTitleBasic(CreateTitleBasicViewModel model)
        {
            var title = _mapper.Map<TitleBasic>(model);

            _dataService.CreateTitleBasic(title);

            return Created(GetUrl(title), CreateTitleBasicViewModel(title));
        }


        private TitleBasicViewModel GetTitleBasicViewModel(Portfolie2.Domain.TitleBasic titleBasic)
        {
            return new TitleBasicViewModel
            {

                //Url = GetUrl(TitleBasicViewModel),
                PrimaryTitle = titleBasic.PrimaryTitle,
                //CategoryId = product.CategoryId
            };
        }

        
        private TitleBasicViewModel CreateTitleBasicViewModel(TitleBasic title)
        {
            var model = _mapper.Map<TitleBasicViewModel>(title);
            model.Url = GetUrl(title);
            model.Id = title.Id;
            return model;
        }
        private string GetUrl(Portfolie2.Domain.TitleBasic titleBasic)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleBasic), new { titleBasic.Id });
        }



    }
}
