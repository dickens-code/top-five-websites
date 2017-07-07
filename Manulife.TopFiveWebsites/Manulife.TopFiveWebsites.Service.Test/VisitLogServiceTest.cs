using Manulife.TopFiveWebsites.Service;
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
using CsvHelper;
using CsvHelper.TypeConversion;

namespace Manulife.TopFiveWebsites.Service.Test
{
    [TestClass]
    public class VisitLogServiceTest
    {
        [TestMethod]
        public void ImportVisitLogSource_WithCsvText_ReturnsVisitLogEntities()
        {
            //set up repo
            var visitLogEntities = new List<VisitLog>();
            var dataStoreRepositoryMock = new Mock<IDataStoreRepository>();
            dataStoreRepositoryMock.Setup(x => x.AddEntity(It.IsAny<VisitLog>())).Callback<VisitLog>(l => visitLogEntities.Add(l));
            dataStoreRepositoryMock.Setup(x => x.SaveChanges()).Returns(() => visitLogEntities.Count);
            var exclusionEntryRepositoryMock = new Mock<IExclusionEntryRepository>();
            var visitLogService = new VisitLogService(exclusionEntryRepositoryMock.Object, dataStoreRepositoryMock.Object);

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

        [TestMethod]
        [ExpectedException(typeof(CsvMissingFieldException), "Fields 'visits' do not exist in the CSV file.")]
        public void ImportVisitLogSource_WithCsvWrongHeading_ThrowsException()
        {
            //set up repo
            var visitLogEntities = new List<VisitLog>();
            var dataStoreRepositoryMock = new Mock<IDataStoreRepository>();
            dataStoreRepositoryMock.Setup(x => x.AddEntity(It.IsAny<VisitLog>())).Callback<VisitLog>(l => visitLogEntities.Add(l));
            var exclusionEntryRepositoryMock = new Mock<IExclusionEntryRepository>();
            var visitLogService = new VisitLogService(exclusionEntryRepositoryMock.Object, dataStoreRepositoryMock.Object);

            //simulate csv data input
            var csvDataReader = new StringReader(@"date|website|vi0sits
2016-01-06|www.bing.com|14065457");

            //parse data and save
            var count = visitLogService.ImportVisitLogSource(csvDataReader);
        }

        [TestMethod]
        [ExpectedException(typeof(CsvTypeConverterException), "The conversion cannot be performed.")]
        public void ImportVisitLogSource_WithCsvWrongDataFormat_ThrowsException()
        {
            //set up repo
            var visitLogEntities = new List<VisitLog>();
            var dataStoreRepositoryMock = new Mock<IDataStoreRepository>();
            dataStoreRepositoryMock.Setup(x => x.AddEntity(It.IsAny<VisitLog>())).Callback<VisitLog>(l => visitLogEntities.Add(l));
            var exclusionEntryRepositoryMock = new Mock<IExclusionEntryRepository>();
            var visitLogService = new VisitLogService(exclusionEntryRepositoryMock.Object, dataStoreRepositoryMock.Object);

            //simulate csv data input
            var csvDataReader = new StringReader(@"date|website|visits
2016-01-06|www.bing.com|1406aa5457");

            //parse data and save
            var count = visitLogService.ImportVisitLogSource(csvDataReader);
        }

        [TestMethod()]
        public void ResetVisitLog_NoParam_InvokesTruncateOnce()
        {
            var dataStoreRepositoryMock = new Mock<IDataStoreRepository>();
            var exclusionEntryRepositoryMock = new Mock<IExclusionEntryRepository>();
            var visitLogService = new VisitLogService(exclusionEntryRepositoryMock.Object, dataStoreRepositoryMock.Object);

            visitLogService.ResetVisitLog();

            dataStoreRepositoryMock.Verify(x => x.TruncateStore<VisitLog>(), Times.Once);
        }

        [TestMethod()]
        public void ResetVisitLogExclusion_NoParam_InvokesTruncateOnce()
        {
            var dataStoreRepositoryMock = new Mock<IDataStoreRepository>();
            var exclusionEntryRepositoryMock = new Mock<IExclusionEntryRepository>();
            var visitLogService = new VisitLogService(exclusionEntryRepositoryMock.Object, dataStoreRepositoryMock.Object);

            visitLogService.ResetVisitLogExclusion();

            dataStoreRepositoryMock.Verify(x => x.TruncateStore<VisitLogExclusion>(), Times.Once);
        }
    }
}
