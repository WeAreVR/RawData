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
        public void Create_Bookmark_ValidData()
        {
            var service = new DataService();
            Bookmark bookmark = new Bookmark()
            {
                Username = "testuser",
                TitleId = "tt7890244 "
            };
            var testBookmark = service.GetBookmark(bookmark.Username, bookmark.TitleId);
            Assert.True(testBookmark.Username != null);
            Assert.Equal("testuser", testBookmark.Username);
            Assert.Equal("tt7890244 ", testBookmark.TitleId);
        }
        [Fact]
        public void DeleteBookmark_ValidId_RemoveTheBookmark()
        {
            var service = new DataService();
            Bookmark bookmark = new Bookmark()
            {
                Username = "testuser",
                TitleId = "tt7890244 "
            };
            service.CreateBookmark(bookmark);
            var result = service.DeleteBookmark(bookmark.Username, bookmark.TitleId);
            Assert.True(result);
            bookmark = service.GetBookmark(bookmark.Username, bookmark.TitleId);
            Assert.Null(bookmark);
        }

        [Fact]
        public void CreateRating_ValidData()
        {
            var service = new DataService();
            RatingHistory ratingHistory = new RatingHistory()
            {
                Username = "testuser",
                TitleId = "tt7890244 ",
                Rating = 5,
                TitleBasic = null,
                User = null
            };
            
            service.CreateRatingHistory(ratingHistory);
            Assert.True(ratingHistory.Username != null);
            Assert.Equal("testuser", ratingHistory.Username);
            Assert.Equal("tt7890244 ", ratingHistory.TitleId);
            Assert.Equal(5, ratingHistory.Rating);
        }
        [Fact]
        public void DeleteRatingHistory()
        {
            var service = new DataService();
            RatingHistory ratingHistory = new RatingHistory()
            {
                Username = "testuser",
                TitleId = "tt7890244 ",
                Rating = 5
            };
             service.CreateRatingHistory(ratingHistory);
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
            Comment testComment = new Comment()
            {
                Username = "testuser",
                TitleId = "tt7890244 ",
                Content = "test comment",
                TimeStamp = new DateTime()
            };
            service.CreateComment(testComment);
            Comment comment = service.GetComment(testComment.Username, testComment.TitleId, testComment.TimeStamp);

            Assert.True(comment.Username != null);
            Assert.Equal("testuser", comment.Username);
            Assert.Equal("tt7890244 ", comment.TitleId);
            Assert.Equal("test comment", comment.Content);
            Assert.Equal(comment.TimeStamp, comment.TimeStamp);
            Assert.Equal(null, comment.TitleBasic);
            Assert.Equal(null, comment.User);

            service.DeleteComment(comment);
        }
        [Fact]
        public void DeleteComment_test()
        {
            var service = new DataService();
            Comment comment = new Comment()
            {
                Username = "testuser",
                TitleId = "tt7890244",
                Content = "test comment",
                TimeStamp = DateTime.Now
            };
            service.CreateComment(comment);
            var result = service.DeleteComment(comment.Username, comment.TitleId, comment.TimeStamp);
            Assert.True(result);
            comment = service.GetComment(comment.Username, comment.TitleId, comment.TimeStamp);
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