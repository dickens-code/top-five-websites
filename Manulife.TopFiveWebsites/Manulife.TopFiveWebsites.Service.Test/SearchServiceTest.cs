using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using Manulife.TopFiveWebsites.Repository;
using Manulife.TopFiveWebsites.Service;
using Manulife.TopFiveWebsites.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Manulife.TopFiveWebsites.Service.Test
{
    [TestClass]
    public class SearchServiceTest
    {
        [TestMethod()]
        public void AggregateByDate_SingleWebsiteInMultipleDates_ReturnsTotalBeforeCriteriaDate()
        {
            var dataStoreRepositoryMock = new Mock<IDataStoreRepository>();
            dataStoreRepositoryMock.Setup(x => x.GetEntities<VisitLog>()).Returns(
                new List<VisitLog>
                {
                    new VisitLog { date = new DateTime(2016, 1, 1), website = "www.google.com", visits = 1000 },
                    new VisitLog { date = new DateTime(2016, 1, 2), website = "www.google.com", visits = 10000 },
                    new VisitLog { date = new DateTime(2016, 1, 3), website = "www.google.com", visits = 100000 },
                    new VisitLog { date = new DateTime(2016, 1, 4), website = "www.google.com", visits = 1000000 },
                }.AsQueryable());

            var searchService = new SearchService(dataStoreRepositoryMock.Object);

            var aggregateRecord = searchService.AggregateByDate(new DateTime(2016, 1, 3), 5);

            Assert.AreEqual(1, aggregateRecord.Count);
            Assert.AreEqual("www.google.com", aggregateRecord[0].Website);
            Assert.AreEqual(111000, aggregateRecord[0].TotalVisits);
        }

        [TestMethod()]
        public void AggregateByDate_MultipleWebsitesInSingleDate_ReturnsTopThree()
        {
            var dataStoreRepositoryMock = new Mock<IDataStoreRepository>();
            dataStoreRepositoryMock.Setup(x => x.GetEntities<VisitLog>()).Returns(
                new List<VisitLog>
                {
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.google.com", visits = 3 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.yahoo.com", visits = 100001 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.bing.com", visits = 5 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.goodluck.com", visits = 100005 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.badluck.com", visits = 100007 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.goforward.com", visits = 100009 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.gobackward.com", visits = 100000 },
                }.AsQueryable());

            var searchService = new SearchService(dataStoreRepositoryMock.Object);

            var aggregateRecord = searchService.AggregateByDate(new DateTime(2016, 1, 6), 3);

            Assert.AreEqual(3, aggregateRecord.Count);
            Assert.AreEqual("www.goforward.com", aggregateRecord[0].Website);
            Assert.AreEqual(100009, aggregateRecord[0].TotalVisits);
            Assert.AreEqual("www.badluck.com", aggregateRecord[1].Website);
            Assert.AreEqual(100007, aggregateRecord[1].TotalVisits);
            Assert.AreEqual("www.goodluck.com", aggregateRecord[2].Website);
            Assert.AreEqual(100005, aggregateRecord[2].TotalVisits);
        }

        [TestMethod()]
        public void AggregateByDate_MultipleCasesWebsitesInSingleDate_ReturnsTopThree()
        {
            var dataStoreRepositoryMock = new Mock<IDataStoreRepository>();
            dataStoreRepositoryMock.Setup(x => x.GetEntities<VisitLog>()).Returns(
                new List<VisitLog>
                {
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.google.com", visits = 3 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.yahoo.com", visits = 100001 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "Www.YAHoO.cOm", visits = 5 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.goodluck.com", visits = 100005 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.badluck.com", visits = 100007 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.goforward.com", visits = 100009 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.gobackward.com", visits = 100000 },
                }.AsQueryable());

            var searchService = new SearchService(dataStoreRepositoryMock.Object);

            var aggregateRecord = searchService.AggregateByDate(new DateTime(2016, 1, 6), 3);

            Assert.AreEqual(3, aggregateRecord.Count);
            Assert.AreEqual("www.goforward.com", aggregateRecord[0].Website);
            Assert.AreEqual(100009, aggregateRecord[0].TotalVisits);
            Assert.AreEqual("www.badluck.com", aggregateRecord[1].Website);
            Assert.AreEqual(100007, aggregateRecord[1].TotalVisits);
            Assert.AreEqual("www.yahoo.com", aggregateRecord[2].Website);
            Assert.AreEqual(100006, aggregateRecord[2].TotalVisits);
        }

        [TestMethod()]
        public void AggregateByDate_MultipleWebsitesInMultipleDates_ReturnsTopThree()
        {
            var dataStoreRepositoryMock = new Mock<IDataStoreRepository>();
            dataStoreRepositoryMock.Setup(x => x.GetEntities<VisitLog>()).Returns(
                new List<VisitLog>
                {
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.google.com", visits = 3 },
                    new VisitLog { date = new DateTime(2016, 1, 7), website = "www.google.com", visits = 10000000 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.yahoo.com", visits = 100001 },
                    new VisitLog { date = new DateTime(2016, 1, 2), website = "www.yahoo.com", visits = 5 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.goodluck.com", visits = 100005 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.badluck.com", visits = 100007 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.goforward.com", visits = 100009 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.gobackward.com", visits = 100000 },
                }.AsQueryable());

            var searchService = new SearchService(dataStoreRepositoryMock.Object);

            var aggregateRecord = searchService.AggregateByDate(new DateTime(2016, 1, 6), 3);

            Assert.AreEqual(3, aggregateRecord.Count);
            Assert.AreEqual("www.goforward.com", aggregateRecord[0].Website);
            Assert.AreEqual(100009, aggregateRecord[0].TotalVisits);
            Assert.AreEqual("www.badluck.com", aggregateRecord[1].Website);
            Assert.AreEqual(100007, aggregateRecord[1].TotalVisits);
            Assert.AreEqual("www.yahoo.com", aggregateRecord[2].Website);
            Assert.AreEqual(100006, aggregateRecord[2].TotalVisits);
        }

        [TestMethod()]
        public void AggregateByDate_MultipleWebsitesInMultipleDatesAndExclusion_ReturnsTopThree()
        {
            var dataStoreRepositoryMock = new Mock<IDataStoreRepository>();
            dataStoreRepositoryMock.Setup(x => x.GetEntities<VisitLog>()).Returns(
                new List<VisitLog>
                {
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.google.com", visits = 3 },
                    new VisitLog { date = new DateTime(2016, 1, 7), website = "www.google.com", visits = 10000000 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.yahoo.com", visits = 100002 },
                    new VisitLog { date = new DateTime(2016, 1, 2), website = "www.yahoo.com", visits = 5 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.goodluck.com", visits = 100001 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.badluck.com", visits = 100007 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.goforward.com", visits = 100009 },
                    new VisitLog { date = new DateTime(2016, 1, 6), website = "www.gobackward.com", visits = 100000 },
                }.AsQueryable());
            dataStoreRepositoryMock.Setup(x => x.GetEntities<VisitLogExclusion>()).Returns(
                new List<VisitLogExclusion>
                {
                    new VisitLogExclusion { host = "www.yahoo.com", excludedSince = new DateTime(2016, 1, 2), excludedTill = new DateTime(2016, 1, 2)},
                    new VisitLogExclusion { host = "www.goodluck.com", excludedSince = new DateTime(2016, 1, 2)}
                }
                .AsQueryable());

            var searchService = new SearchService(dataStoreRepositoryMock.Object);

            var aggregateRecord = searchService.AggregateByDate(new DateTime(2016, 1, 6), 4);

            Assert.AreEqual(4, aggregateRecord.Count);
            Assert.AreEqual("www.goforward.com", aggregateRecord[0].Website);
            Assert.AreEqual(100009, aggregateRecord[0].TotalVisits);
            Assert.AreEqual("www.badluck.com", aggregateRecord[1].Website);
            Assert.AreEqual(100007, aggregateRecord[1].TotalVisits);
            Assert.AreEqual("www.yahoo.com", aggregateRecord[2].Website);
            Assert.AreEqual(100002, aggregateRecord[2].TotalVisits);
            Assert.AreEqual("www.gobackward.com", aggregateRecord[3].Website);
            Assert.AreEqual(100000, aggregateRecord[3].TotalVisits);
        }
    }
}
