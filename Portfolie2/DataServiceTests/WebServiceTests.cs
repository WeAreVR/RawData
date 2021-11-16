using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Portfolie2.Domain;
using WebService.Controllers;
using WebService.ViewModels;
using Xunit;
using Portfolie2;

namespace DataServiceTests
{
    public class CommentsControllerTest
    {
        private readonly Mock<IDataService> _dataServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<LinkGenerator> _linkGeneratorMock;

        private const string CommentsApi = "http://localhost:5001/api/comments";

        /* /api/comments */

        public CommentsControllerTest()
        {
            _dataServiceMock = new Mock<IDataService>();
            _mapperMock = new Mock<IMapper>();
            _linkGeneratorMock = new Mock<LinkGenerator>();
        }

        [Fact]
        public void ApiComments_GetWithValidTitleId_OkAndComments()
        {
            var (comment, statusCode) = GetObject($"{CommentsApi}/tt0926084");

            Assert.Equal(HttpStatusCode.OK, statusCode);
            Assert.Equal(8, comment["items"].Count());
            Assert.Equal("Harry Potter and the Deathly Hallows: Part 1", comment["items"].First()["primaryTitle"]);
        }
        
        [Fact]
        public void ApiComments_GetWithInvalidTitleyId_NotFound()
        {
            var (_, statusCode) = GetObject($"{CommentsApi}/0");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public void CreateComment_ValidNewComment_DataServiceCreateCommentMustBeCalledOnce()
        {

            _mapperMock.Setup(x => x.Map<Comment>(It.IsAny<CreateCommentViewModel>())).Returns(new Comment());
            _mapperMock.Setup(x => x.Map<CommentViewModel>(It.IsAny<Comment>())).Returns(new CommentViewModel());

            _linkGeneratorMock.Setup(x => x.GetUriByAddress(
                    It.IsAny<HttpContext>(),
                    It.IsAny<string>(),
                    It.IsAny<RouteValueDictionary>(),
                    default, default, default, default, default, default))
                .Returns("");

            var ctrl = new CommentController(_dataServiceMock.Object, _linkGeneratorMock.Object, _mapperMock.Object);
            ctrl.ControllerContext = new ControllerContext();
            ctrl.ControllerContext.HttpContext = new DefaultHttpContext();


            ctrl.CreateComment(new CreateCommentViewModel());

            _dataServiceMock.Verify(x => x.CreateComment(It.IsAny<Comment>()), Times.Once);

        }



        /*


        [Fact]
        public void ApiComments_PostWithComment_Created()
        {
            var newComment = new
            {
                Username = "Fakeuser123",
                TitleId = "12345678",
                Content = "Test comment.",
                TimeStamp = DateTime.Now

            };
            var (comment, statusCode) = PostData(CommentsApi, newComment);

            Assert.Equal(HttpStatusCode.Created, statusCode);

            //DeleteData($"{CommentsApi}/{comment["username"]}/{comment["titleId"]}/{comment["timeStamp"]}");
        }
        
        [Fact]
        public void ApiCategories_PutWithValidCategory_Ok()
        {

            var data = new
            {
                Name = "Created",
                Description = "Created"
            };
            var (category, _) = PostData($"{CategoriesApi}", data);

            var update = new
            {
                Id = category["id"],
                Name = category["name"] + "Updated",
                Description = category["description"] + "Updated"
            };

            var statusCode = PutData($"{CategoriesApi}/{category["id"]}", update);

            Assert.Equal(HttpStatusCode.OK, statusCode);

            var (cat, _) = GetObject($"{CategoriesApi}/{category["id"]}");

            Assert.Equal(category["name"] + "Updated", cat["name"]);
            Assert.Equal(category["description"] + "Updated", cat["description"]);

            DeleteData($"{CategoriesApi}/{category["id"]}");
        }

        [Fact]
        public void ApiCategories_PutWithInvalidCategory_NotFound()
        {
            var update = new
            {
                Id = -1,
                Name = "Updated",
                Description = "Updated"
            };

            var statusCode = PutData($"{CategoriesApi}/-1", update);

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }

        [Fact]
        public void ApiCategories_DeleteWithValidId_Ok()
        {

            var data = new
            {
                Name = "Created",
                Description = "Created"
            };
            var (category, _) = PostData($"{CategoriesApi}", data);

            var statusCode = DeleteData($"{CategoriesApi}/{category["id"]}");

            Assert.Equal(HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void ApiCategories_DeleteWithInvalidId_NotFound()
        {

            var statusCode = DeleteData($"{CategoriesApi}/-1");

            Assert.Equal(HttpStatusCode.NotFound, statusCode);
        }




        */


        // Helpers

        (JArray, HttpStatusCode) GetArray(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JArray)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) GetObject(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        (JObject, HttpStatusCode) PostData(string url, object content)
        {
            var client = new HttpClient();
            var requestContent = new StringContent(
                JsonConvert.SerializeObject(content),
                Encoding.UTF8,
                "application/json");
            var response = client.PostAsync(url, requestContent).Result;
            var data = response.Content.ReadAsStringAsync().Result;
            return ((JObject)JsonConvert.DeserializeObject(data), response.StatusCode);
        }

        HttpStatusCode PutData(string url, object content)
        {
            var client = new HttpClient();
            var response = client.PutAsync(
                url,
                new StringContent(
                    JsonConvert.SerializeObject(content),
                    Encoding.UTF8,
                    "application/json")).Result;
            return response.StatusCode;
        }

        HttpStatusCode DeleteData(string url)
        {
            var client = new HttpClient();
            var response = client.DeleteAsync(url).Result;
            return response.StatusCode;
        }
    }
}
