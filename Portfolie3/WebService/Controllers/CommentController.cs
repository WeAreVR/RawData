
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

        [HttpGet(Name = nameof(GetComments))]
        public IActionResult GetComments(string titleId, [FromQuery] QueryString queryString)
        {
            var comments = _dataService.GetCommentsByTitleId(titleId, queryString);

            if (comments.Count == 0)
            {
                return NotFound();
            }

            var allComments = _dataService.GetCommentsByTitleId(titleId);

            var items = comments.Select(GetCommentViewModel);

            var result = CreateResultModel(titleId, queryString, _dataService.NumberOfElements(allComments), items);

            return Ok(result);

        }

        [HttpDelete]
        public IActionResult DeleteComment(Comment comment)
        {
            Console.WriteLine("nu sker der noget mske");
            if (!_dataService.DeleteComment(comment))
            {
                return NotFound();
            }
            _dataService.DeleteComment(comment);
            return NoContent();
        }


        [HttpDelete("{username}/{titleId}/{timeStamp}")]
        public IActionResult DeleteComment(string titleId, string username, DateTime timeStamp)
        {
            if (!_dataService.DeleteComment(username, titleId, timeStamp))
            {
                return NotFound();
            }
            _dataService.DeleteComment(username, titleId, timeStamp);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateComment(CreateCommentViewModel model)
        {
            var comment = _mapper.Map<Comment>(model);

            _dataService.CreateComment(comment);

            return Created(GetUrl(comment), CreateCommentViewModel(comment));
        }
       


        [HttpPut]
        public IActionResult UpdateComment(CommentViewModel model)
        {
            var comment = new Comment
            {
                Username = model.Username,
                TitleId = model.TitleId,
                Content = model.Content,
                TimeStamp = DateTime.Now
            };

            if (!_dataService.UpdateComment(comment))
            {
                return NotFound();
            }

            return NoContent();
        }


        //Helper methods


        private CommentViewModel GetCommentViewModel(Comment comment)
        {
            var model = _mapper.Map<CommentViewModel>(comment);
            model.Url = GetUrl(comment);
            model.PrimaryTitle = comment.TitleBasic.PrimaryTitle;
            return model;
        }

        private CommentViewModel CreateCommentViewModel(Comment comment)
        {
            var model = _mapper.Map<CommentViewModel>(comment);
            model.Url = GetUrl(comment);
            return model;
        }

        private string GetUrl(Comment comment)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetComments), new { comment.TitleId });
        }

        private string GetUrl(string titleId, int page, int pageSize)
        {
            return _linkGenerator.GetUriByName(
                HttpContext,
                nameof(GetComments),
                new { titleId, page, pageSize });
        }


        private object CreateResultModel(string titleId, QueryString queryString, int total, IEnumerable<CommentViewModel> model)
        {
            return new
            {
                total,
                prev = CreateNextPageLink(titleId, queryString),
                cur = CreateCurrentPageLink(titleId, queryString),
                next = CreateNextPageLink(titleId, queryString, total),
                items = model
            };
        }

        private string CreateNextPageLink(string titleId, QueryString queryString, int total)
        {
            var lastPage = GetLastPage(queryString.PageSize, total);
            return queryString.Page >= lastPage ? null : GetUrl(titleId, queryString.Page + 1, queryString.PageSize);
        }


        private string CreateCurrentPageLink(string titleId, QueryString queryString)
        {
            return GetUrl(titleId, queryString.Page, queryString.PageSize);
        }

        private string CreateNextPageLink(string titleId, QueryString queryString)
        {
            return queryString.Page <= 0 ? null : GetUrl(titleId, queryString.Page - 1, queryString.PageSize);
        }

        private static int GetLastPage(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize) - 1;
        }
    }
}
