
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
            var comments = _dataService.GetCommentsByTitleId("tt0312280", queryString);

            if (comments.Count == 0)
            {
                return NotFound();
            }

            var allComments = _dataService.GetCommentsByTitleId("tt0312280");

            var items = comments.Select(GetCommentViewModel);

            var result = CreateResultModel("tt0312280", queryString, _dataService.NumberOfElements(allComments), items);

            return Ok(result);

        }

        [HttpDelete]
        public IActionResult DeleteComment(Comment comment)
        {
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

        private string GetUrl(string tid, int page, int pageSize)
        {
            return _linkGenerator.GetUriByName(
                HttpContext,
                nameof(GetComments),
                new {tid, page, pageSize });
        }


        private object CreateResultModel(string tid, QueryString queryString, int total, IEnumerable<CommentViewModel> model)
        {
            return new
            {
                total,
                prev = CreateNextPageLink(tid, queryString),
                cur = CreateCurrentPageLink(tid, queryString),
                next = CreateNextPageLink(tid, queryString, total),
                items = model
            };
        }

        private string CreateNextPageLink(string tid, QueryString queryString, int total)
        {
            var lastPage = GetLastPage(queryString.PageSize, total);
            return queryString.Page >= lastPage ? null : GetUrl(tid, queryString.Page + 1, queryString.PageSize);
        }


        private string CreateCurrentPageLink(string tid, QueryString queryString)
        {
            return GetUrl(tid, queryString.Page, queryString.PageSize);
        }

        private string CreateNextPageLink(string tid, QueryString queryString)
        {
            return queryString.Page <= 0 ? null : GetUrl(tid, queryString.Page - 1, queryString.PageSize);
        }

        private static int GetLastPage(int pageSize, int total)
        {
            return (int)Math.Ceiling(total / (double)pageSize) - 1;
        }
    }
}
