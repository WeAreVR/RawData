using Portfolie2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.ViewModels;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/titlebasic")]
    public class TitleBasicController : Controller
    {

        IDataService _dataService;
        LinkGenerator _linkGenerator;

        public TitleBasicController(IDataService dataService, LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public IActionResult GetTitleBasic()
        {
            var titleBasic = _dataService.GetTitleBasic("tt10850888");

            if (titleBasic == null)
            {
                return NotFound();
            }

            TitleBasicViewModel model = GetTitleViewModel(titleBasic);

            return Ok(model);
        }
        private TitleBasicViewModel GetTitleViewModel(Portfolie2.Domain.TitleBasic titleBasic)
        {
            return new TitleBasicViewModel
            {

                //Url = GetUrl(TitleBasicViewModel),
                PrimaryTitle = titleBasic.PrimaryTitle,
                //CategoryId = product.CategoryId
            };
        }

        private string GetUrl(Portfolie2.Domain.TitleBasic titleBasic)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetTitleBasic), new { titleBasic.Id });
        }
    }
}
