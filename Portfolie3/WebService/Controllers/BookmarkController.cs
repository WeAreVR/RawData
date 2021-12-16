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
using WebService.Attributes;


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

        //[Authorization]
        [HttpGet(Name = nameof(GetBookmarks))]
        public IActionResult GetBookmarks(string username, [FromQuery] QueryString queryString)
        {

            var user = HttpContext.User.Identity.Name;

            var bookmarks = _dataService.GetBookmarks(username, queryString);

            if (bookmarks.Count() == 0)
            {
                return NotFound();
            }

            var allBookmarks = _dataService.GetBookmarks(username);
            var items = bookmarks.Select(GetBookmarkViewModel);
            var result = CreateResultModel(queryString, _dataService.NumberOfElements(allBookmarks), items);

            return Ok(result);
        }

        [HttpGet("{toggle}")]
        public IActionResult ToggleBookmark(string username, string titleId) {

            var result = _dataService.BookmarkedAlready(username, titleId);

            if (!result) {
                Console.WriteLine(result);
                _dataService.CreateBookmark(username, titleId);
                return Ok();
            }

            Console.WriteLine(result);
            _dataService.DeleteBookmark(username, titleId);
            return Ok();
        }



        [HttpDelete()]
        public IActionResult DeleteBookMark(string username, string titleId)
        {

            _dataService.DeleteBookmark(username, titleId);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateBookMark(string username, string titleId)
        {
            Console.WriteLine("IDDDDDDDDDDDDD" + titleId);
            _dataService.CreateBookmark(username, titleId);

            return NoContent();
        }




        //Helper methods
 
        private BookmarkViewModel GetBookmarkViewModel(Bookmark bookmark)
        {
            var model = _mapper.Map<BookmarkViewModel>(bookmark);
            model.Url = GetUrl(bookmark);
            model.TitleId = bookmark.TitleId;
            model.PrimaryTitle = bookmark.TitleBasic.PrimaryTitle;
            return model;
        }
        private string GetUrl(Bookmark bookmark)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetBookmarks), new { bookmark.TitleId });
        }

        private string GetUrl(int page, int pageSize)
        {
            return _linkGenerator.GetUriByName(
                HttpContext,
                nameof(GetBookmarks),
                new { page, pageSize });
        }


        private object CreateResultModel(QueryString queryString, int total, IEnumerable<BookmarkViewModel> model)
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
            return queryString.Page >= lastPage ? null : GetUrl(queryString.Page + 1, queryString.PageSize);
        }


        private string CreateCurrentPageLink(QueryString queryString)
        {
            return GetUrl(queryString.Page, queryString.PageSize);
        }

        private string CreateNextPageLink(QueryString queryString)
        {
            return queryString.Page <= 0 ? null : GetUrl(queryString.Page - 1, queryString.PageSize);
        }

        private static int GetLastPage(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize) - 1;
        }
    }
}
