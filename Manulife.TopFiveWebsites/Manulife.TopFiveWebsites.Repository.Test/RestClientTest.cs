using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Manulife.TopFiveWebsites.Repository.Test
{
    [TestClass()]
    public class RestClientTest
    {
        private readonly Uri _baseAddress = new Uri("http://private-1de182-mamtrialrankingadjustments4.apiary-mock.com/");

        [TestMethod()]
        public void Execute_SpecificResource_ReturnsCorrectContent()
        {
            var restClient = new RestClient(_baseAddress);
            var request = new RestRequest("exclusions");
            var response = restClient.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(204, response.ContentLength);
        }
    }
}
