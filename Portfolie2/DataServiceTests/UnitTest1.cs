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
        /* Categories */

        [Fact]
        public void Category_Object_HasIdNameAndDescription()
        {
            var titlebasic = new TitleBasic();
            Assert.Equal(0, titlebasic.Runtime);
            Assert.Null(titlebasic.PrimaryTitle);
            Assert.Null(titlebasic.OriginalTitle);
        }

        [Fact]
        public void CreateTitleBasic_ValidData_CreateTitleBasicAndRetunsNewObject()
        {
            var service = new DataService();
            var titlebasic = service.CreateTitleBasic("TESTID", "TestTitle",false);
            Assert.True(titlebasic.Id != null);
            Assert.Equal("TestTitle", titlebasic.PrimaryTitle);
            Assert.Equal("TESTID", titlebasic.Id);
            Assert.False(titlebasic.IsAdult);

            // cleanup
            //service.DeleteTitleBasic(titlebasic.Id);
        }

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
    }
}