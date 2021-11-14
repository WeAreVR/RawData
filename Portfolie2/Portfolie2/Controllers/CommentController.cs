
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
    [Route("api/comment")]
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

        [HttpGet]
        public IActionResult GetComment()
        {
            var comment = _dataService.GetComment("tt10850888","test");

            if (comment == null)
            {
                return NotFound();
            }

            CommentViewModel model = GetCommentViewModel(comment);

            return Ok(model);
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

            return Created(GetUrl(title), CreateCommentViewModel(title));
        }


        private CommentViewModel GetCommentViewModel(Comment comment)
        {
            return new CommentViewModel
            {
                Username = comment.Username,
                TitleId = comment.TitleId,
                Content = comment.Content
            };
        }


        private CommentViewModel CreateCommentViewModel(Comment comment)
        {
            var model = _mapper.Map<CommentViewModel>(comment);
            model.Url = GetUrl(comment);
            model.TitleId = comment.TitleId;
            return model;
        }
        private string GetUrl(Comment comment)
        {
            return _linkGenerator.GetUriByName(HttpContext, nameof(GetComment), new { comment.TitleId });
        }



    }
}
