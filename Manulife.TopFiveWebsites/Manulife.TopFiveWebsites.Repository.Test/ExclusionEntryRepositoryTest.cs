using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Moq;
using RestSharp;

using Manulife.TopFiveWebsites.Repository;

namespace Manulife.TopFiveWebsites.Repository.Test
{
    [TestClass()]
    public class ExclusionEntryRepositoryTest
    {
        [TestMethod()]
        public void GetEntities_WithJsonText_ReturnsExclusionEntryList()
        {
            var restClientMock = new Mock<IRestClient>();
            restClientMock.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(new RestResponse
            {
                Content = "[{\"host\": \"facebook.com\",\"excludedSince\": \"2016-12-01\"},{\"host\": \"google.com\",\"excludedSince\": \"2016-03-12\",\"excludedTill\": \"2016-03-14\"}]"
            });

            var exclusionEntryRepo = new ExclusionEntryRepository(restClientMock.Object);

            var exclusionEntryList = exclusionEntryRepo.GetEntities<ExclusionEntry>().ToList();

            Assert.AreEqual(2, exclusionEntryList.Count);
            Assert.AreEqual("facebook.com", exclusionEntryList[0].host);
            Assert.AreEqual("google.com", exclusionEntryList[1].host);
        }
    }
}