using Assignment4;
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
    [Route("api/categories")]
    public class CategoriesController : Controller
    {

        IDataService _dataService;
        LinkGenerator _linkGenerator;

        public CategoriesController(IDataService dataService, LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _dataService.GetCategories();

            return Ok(categories.Select(x => GetCategoryViewModel(x)));
        }

        // api/categories/id
        [HttpGet("{id}", Name = nameof(GetCategory))]
        public IActionResult GetCategory(int id)
        {
            var category = _dataService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryViewModel model = GetCategoryViewModel(category);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryViewModel model)
        {
            var category = new Assignment4.Domain.Category
            {
                Name = model.Name,
                Description = model.Description
            };

            _dataService.CreateCategory(category);

            return Created(GetUrl(category), GetCategoryViewModel(category));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, CreateCategoryViewModel model)
        {
            var category = new Assignment4.Domain.Category
            {
                Id = id,
                Name = model.Name,
                Description = model.Description
            };

            if (!_dataService.UpdateCategory(category))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if(!_dataService.DeleteCategory(id))
            {
                return NotFound();
            }
            return NoContent();
        }


        private CategoryViewModel GetCategoryViewModel(Assignment4.Domain.Category category)
        {
            return new CategoryViewModel
            {

                Url = GetUrl(category),
                Name = category.Name,
                Desc = category.Description
            };
        }

        private string GetUrl(Assignment4.Domain.Category category)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetCategory), new { category.Id });
        }


    }
}
