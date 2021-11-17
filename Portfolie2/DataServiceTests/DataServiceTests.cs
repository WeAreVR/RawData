using System;
using System.Linq;
using Portfolie2.Domain;
using Xunit;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace Portfolie2.DataServiceTests
{
    public class DataServiceTests

    {
        /* Title */

        [Fact]
        public void TitleBasic_Object_HasPrimaryTitleAndOriginalTitle()
        {
            var titlebasic = new TitleBasic();
            Assert.Equal(0, titlebasic.TitleRating.AvgRating);
            Assert.Null(titlebasic.PrimaryTitle);
            Assert.Null(titlebasic.OriginalTitle);
        }

        [Fact]
        public void CreateTitleBasic_ValidData_CreateTitleBasicAndRetunsNewObject()
        {
            var service = new DataService();
            var titlebasic = service.CreateTitleBasic("TESTID", "TestTitle", false);
            Assert.True(titlebasic.Id != null);
            Assert.Equal("TestTitle", titlebasic.PrimaryTitle);
            Assert.Equal("TESTID", titlebasic.Id);
            Assert.False(titlebasic.IsAdult);

            // cleanup
            service.DeleteTitleBasic(titlebasic.Id);
        }
        /*
         [Fact]
         public void UpdateTitle()
         {
             var service = new DataService();
             var titlebasic = service.CreateTitleBasic();
         }

         */
        [Fact]
        public void DeleteCategory_ValidId_RemoveTheCategory()
        {
            var service = new DataService();
            var titlebasic = service.CreateTitleBasic("TestIDD", "DeleteTest", true);
            var result = service.DeleteTitleBasic(titlebasic.Id);
            Assert.True(result);
            titlebasic = service.GetTitleBasic(titlebasic.Id);
            Assert.Null(titlebasic);
        }
        [Fact]
        public void CreateNameBasic_ValidData_CreateNameBasicAndRetunsNewObject()
        {
            var service = new DataService();
            var namebasic = service.CreateNameBasic("TESTID", "TestName");
            Assert.True(namebasic.Id != null);
            Assert.Equal("TestName", namebasic.PrimaryName);
            Assert.Equal("TESTID", namebasic.Id);

            // cleanup
            service.DeleteNameBasic(namebasic.Id);
        }
        [Fact]
        public void Create_Bookmark_ValidData()
        {
            var service = new DataService();
            Bookmark bookmark = new Bookmark()
            {
                Username = "Per",
                TitleId = "something"
            };
            var testBookmark = service.CreateBookMark(bookmark);
            Assert.True(bookmark.Username != null);
            Assert.Equal("Per", bookmark.Username);
            Assert.Equal("something", bookmark.TitleId);
        }
        [Fact]
        public void DeleteBookmark_ValidId_RemoveTheBookmark()
        {
            var service = new DataService();
            Bookmark bookmark = new Bookmark()
            {
                Username = "per",
                TitleId = "something"
            };
            var testBookmark = service.CreateBookMark(bookmark);
            var result = service.DeleteTitleBasic(bookmark.TitleId);
            Assert.True(result);
            bookmark = service.GetBookmark(bookmark.Username, "tt0926084");
            Assert.Null(bookmark);
        }
        [Fact]
        public void CreateRating_ValidData()
        {
            var service = new DataService();
            RatingHistory ratingHistory = new RatingHistory()
            {
                Username = "Per",
                TitleId = "something",
                Rating = 5,
                TitleBasic = null,
                User = null
            };
            var testRatingHistory = service.CreateRatingHistory(ratingHistory);
            Assert.True(ratingHistory.Username != null);
            Assert.Equal("Per", ratingHistory.Username);
            Assert.Equal("something", ratingHistory.TitleId);
            Assert.Equal(5, ratingHistory.Rating);
        }
        [Fact]
        public void DeleteRatingHistory()
        {
            var service = new DataService();
            RatingHistory ratingHistory = new RatingHistory()
            {
                Username = "Per",
                TitleId = "something",
                Rating = 5,
                TitleBasic = null,
                User = null
            };
            var testRatingHistory = service.CreateRatingHistory(ratingHistory);
            var result = service.DeleteRatingHistory(ratingHistory.Username, ratingHistory.TitleId);
            Assert.True(result);
            ratingHistory = service.GetRatingHistory(ratingHistory.Username, ratingHistory.TitleId);
            Assert.Null(ratingHistory);
        }
        /// <summary>
        /// COMMENTS!!!!!
        /// </summary>
        [Fact]
        public void CreateComment()
        {
            var service = new DataService();
            Comment comment = new Comment()
            {
                Username = "Per",
                TitleId = "something",
                Content = "test Tekst",
                TimeStamp = new DateTime(),
                TitleBasic = null,
                User = null
            };
            var testComment = service.CreateComment(comment);
            Assert.True(comment.Username != null);
            Assert.Equal("Per", comment.Username);
            Assert.Equal("something", comment.TitleId);
            Assert.Equal("test Tekst", comment.Content);
            Assert.Equal(new DateTime(), comment.TimeStamp);
            Assert.Equal(null, comment.TitleBasic);
            Assert.Equal(null, comment.User);
        }
        [Fact]
        public void DeleteComment_test()
        {
            var service = new DataService();
            Comment comment = new Comment()
            {
                Username = "Per",
                TitleId = "something",
                Content = "test Tekst",
                TimeStamp = new DateTime(),
                TitleBasic = null,
                User = null
            };
            service.CreateComment(comment);
            var result = service.DeleteComment(comment.Username, comment.TitleId, comment.TimeStamp);
            Assert.True(result);
            comment = service.GetComment(comment.Username, comment.TitleId);
            Assert.Null(comment);
        }
        /// <summary>
        /// SEARCHHISTORY TEST!!!!
        /// </summary>
        /*
        [Fact]
        public void DeleteSearchHistory_ValidId_RemoveTheSearchHistory()
        {
            var service = new DataService();
            var searchhistory = service.CreateSearchHistory("TestIDD", "DeleteTest", true);
            var result = service.DeleteTitleBasic(titlebasic.Id);
            Assert.True(result);
            titlebasic = service.GetTitleBasic(titlebasic.Id);
            Assert.Null(titlebasic);
        }*/

    }
}