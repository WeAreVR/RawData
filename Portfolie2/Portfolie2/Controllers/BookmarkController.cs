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
        [HttpGet("{username}", Name = nameof(GetBookmarks))]
        public IActionResult GetBookmarks(string username, [FromQuery] QueryString queryString)
        {

            //var user = Request.HttpContext.Items["User"] as User;
            //var bookmarks = _dataService.GetBookmarks(username);
            var bookmarks = _dataService.GetBookmarks(username, queryString);
            if (bookmarks == null)
            {
                return NotFound();
            }

            var numberOfBookmarks = bookmarks.Count();

            var items = bookmarks.Select(GetBookmarkViewModel);
            var result = CreateResultModel(queryString, numberOfBookmarks, items);
            //BookmarkViewModel model = CreateBookmarkViewModel(bookmarks);
            return Ok(result);
        }  
            
           

        [HttpDelete("{username}/{titleId}")]
        public IActionResult DeleteBookMark(string username, string titleId)
        {
            _dataService.DeleteBookMark(username,titleId);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateBookMark(BookmarkViewModel model)
        {
            var bookmark = _mapper.Map<Bookmark>(model);

            _dataService.CreateBookMark(bookmark);

            return Created(GetUrl(bookmark), GetBookmarkViewModel(bookmark));
        }




        //Helper methods
 
        private BookmarkViewModel GetBookmarkViewModel(Bookmark bookmark)
        {
            var model = _mapper.Map<BookmarkViewModel>(bookmark);
            model.Url = GetUrl(bookmark);
            model.TitleId = bookmark.TitleId;
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
