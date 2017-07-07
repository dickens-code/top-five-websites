using Manulife.TopFiveWebsites.Entity;
using Manulife.TopFiveWebsites.Repository;
using Manulife.TopFiveWebsites.Repository.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manulife.TopFiveWebsites.Service.Test
{
    [TestClass]
    public class VisitLogServiceTest
    {
        [TestMethod]
        public void ImportVisitLogSource_WithCsvText_ReturnsVisitLogEntities()
        {
            //set up data store repo
            var visitLogEntities = new List<VisitLog>();
            var dataStoreRepositoryMock = new Mock<IDataStoreRepository>();
            dataStoreRepositoryMock.Setup(x => x.AddEntity(It.IsAny<VisitLog>())).Callback<VisitLog>(l => visitLogEntities.Add(l));
            dataStoreRepositoryMock.Setup(x => x.SaveChanges()).Returns(() => visitLogEntities.Count);

            //set up exclusion entry repo
            var exclusionEntryRepositoryMock = new Mock<IExclusionEntryRepository>();

            //simulate csv data input
            var csvDataReader = new StringReader(@"date|website|visits
2016-01-06|www.bing.com|14065457
2016-01-06|www.ebay.com.au|19831166
2016-01-06|www.facebook.com|104346720
2016-01-06|mail.live.com|21536612
2016-01-06|www.wikipedia.org|13246531
2016-01-27|www.ebay.com.au|23154653
2016-01-06|au.yahoo.com|11492756
2016-01-06|www.google.com|26165099");

            //parse data and save
            var visitLogService = new VisitLogService(exclusionEntryRepositoryMock.Object, dataStoreRepositoryMock.Object);
            var count = visitLogService.ImportVisitLogSource(csvDataReader);

            Assert.AreEqual(visitLogEntities.Count, count);
            Assert.AreEqual(8, visitLogEntities.Count);
            Assert.AreEqual(new DateTime(2016, 1, 6), visitLogEntities[0].date);
            Assert.AreEqual("www.bing.com", visitLogEntities[0].website);
            Assert.AreEqual(14065457, visitLogEntities[0].visits);
            Assert.AreEqual(new DateTime(2016, 1, 27), visitLogEntities[5].date);
            Assert.AreEqual("www.ebay.com.au", visitLogEntities[5].website);
            Assert.AreEqual(23154653, visitLogEntities[5].visits);
        }
    }
}
