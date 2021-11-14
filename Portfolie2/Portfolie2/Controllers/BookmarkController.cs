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
    [Route("api/bookmark")]
    public class BookmarkController : Controller
    {

        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public BookmarkController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult getBookmark()
        {
            var bookmark = _dataService.GetBookmark("per");
            if (bookmark == null)
            {
                return NotFound();
            }
            BookmarkViewModel model = GetBookmarkViewModel(bookmark);
            return Ok(model);
        }

        private BookmarkViewModel GetBookmarkViewModel(Bookmark bookmark)
        {
            return new BookmarkViewModel
            {
                TileId = bookmark.TitleId
            };
        }

        [HttpDelete("{titleId}")]
        public IActionResult DeleteBookMark(string titleId)
        {
            _dataService.DeleteBookMark(titleId);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateBookMark(BookmarkViewModel model)
        {
            var bookmark = _mapper.Map<Bookmark>(model);

            _dataService.CreateBookMark(bookmark);

            return Created(GetUrl(bookmark), CreateBookMarkViewModel(bookmark));
        }
        private BookmarkViewModel CreateBookMarkViewModel(Bookmark bookmark)
        {
            var model = _mapper.Map<BookmarkViewModel>(bookmark);
            model.TileId = bookmark.TitleId;
            return model;
        }
        private string GetUrl(Portfolie2.Domain.Bookmark bookmark)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(getBookmark), new { bookmark.TitleId });
        }
    }
}
