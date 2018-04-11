using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using MerchantPortal.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MerchantPortal.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task GetApiForAll()
        {
            // Arrange          
            var controller = new DataController();

            // Act
            IHttpActionResult actionResult = controller.Get();

            // Assert
            Assert.IsNotNull(actionResult);

           
        }

        [TestMethod]
        public async Task GetAuthenticate()
        {
            // Arrange          
            var controller = new DataController();

            // Act
            IHttpActionResult actionResult = controller.GetForAuthenticate();

            String content = ((System.Web.Http.Results.OkNegotiatedContentResult<string>)actionResult).Content;
            // Assert
            if (content.Contains("Hi"))
            {
                Assert.IsNotNull(actionResult);
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(true);
            }
            
        }
    }
}
