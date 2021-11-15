
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
    [Route("api/comments")]
    public class CommentController : Controller
    {

        private readonly IDataService _dataService;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;

        public CommentController(IDataService dataService, LinkGenerator linkGenerator, IMapper mapper)
        {
            _dataService = dataService;
            _linkGenerator = linkGenerator;
            _mapper = mapper;
        }

        [HttpGet("{titleId}", Name = nameof(GetComments))]
        public IActionResult GetComments(string titleId, [FromQuery] QueryString queryString)
        {
            var comments = _dataService.GetCommentsByTitleId(titleId, queryString);
            
            if (comments == null)
            {
                return NotFound();
            }

            var numberOfComments = comments.Count();

            var items = comments.Select(GetCommentViewModel);
            var result = CreateResultModel(queryString, numberOfComments, items);

            return Ok(result);
            
        }


        [HttpDelete("{username}/{id}")]
        public IActionResult DeleteComment(string username, string id)
        {
            /*if (!_dataService.DeleteTitleBasic(id))
            {
                return NotFound();
            }*/
            _dataService.DeleteComment(username,id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateComment(CreateCommentViewModel model)
        {
            var title = _mapper.Map<Comment>(model);

            _dataService.CreateComment(title);   // titles ??

            return Created(GetUrl(title), GetCommentViewModel(title));
        }


        //Helper methods


        private CommentViewModel GetCommentViewModel(Comment comment)
        {
            var model = _mapper.Map<CommentViewModel>(comment);
            model.Url = GetUrl(comment);
            model.TitleId = comment.TitleId;
            model.PrimaryTitle = comment.TitleBasic.PrimaryTitle;
            return model;
        }
        private string GetUrl(Comment comment)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetComments), new { comment.TitleId });
        }

        private string GetUrl(int page, int pageSize)
        {
            return _linkGenerator.GetUriByName(
                HttpContext,
                nameof(GetComments),
                new { page, pageSize });
        }


        private object CreateResultModel(QueryString queryString, int total, IEnumerable<CommentViewModel> model)
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
