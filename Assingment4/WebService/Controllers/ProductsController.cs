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
    [Route("api/products")]
    public class ProductsController : Controller
    {

        IDataService _dataService;
        LinkGenerator _linkGenerator;

        public ProductsController(IDataService dataService, LinkGenerator linkGenerator)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _dataService.GetProducts();

            return Ok(products.Select(x => GetProductViewModel(x)));
        }

        // api/products/id
        [HttpGet("{id}", Name = nameof(GetProduct))]
        public IActionResult GetProduct(int id)
        {
            var product = _dataService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            ProductViewModel model = GetProductViewModel(product);

            return Ok(model);
        }


        // api/products/name/substring
        [HttpGet("name/{substring}", Name = nameof(GetProductByName))]
        public IActionResult GetProductByName(string substring)
        {
            var products = _dataService.GetProductByName(substring);

            if (products == null)
            {
                return NotFound();
            }


            return Ok(products.Select(x => GetProductViewModel(x)));
        }


        private ProductViewModel GetProductViewModel(Assignment4.Domain.Product product)
        {
            return new ProductViewModel
            {

                Url = GetUrl(product),
                Name = product.Name,
                CategoryId = product.CategoryId
            };
        }

        private string GetUrl(Assignment4.Domain.Product product)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetProduct), new { product.Id });
        }


    }
}
