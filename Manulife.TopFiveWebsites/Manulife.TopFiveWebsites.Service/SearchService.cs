﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Manulife.TopFiveWebsites.Entity;
using Manulife.TopFiveWebsites.Service.Interface;
using Manulife.TopFiveWebsites.Repository.Interface;

namespace Manulife.TopFiveWebsites.Service
{
    public class SearchService : ISearchService
    {
        protected readonly IDataStoreRepository _dataStoreRepository;

        public SearchService(IDataStoreRepository dataStoreRepository)
        {
            _dataStoreRepository = dataStoreRepository;
        }

        public IList<WebsiteStatistics> AggregateByDate(DateTime date, int topX)
        {
            var visitLogEntities = _dataStoreRepository.GetEntities<VisitLog>();
            var visitLogExclusionEntities = _dataStoreRepository.GetEntities<VisitLogExclusion>();

            //filter out invalid logs
            var validLogs = from log in visitLogEntities
                            where log.date <= date.Date
                                && !visitLogExclusionEntities.Any(
                                    ex => ex.host == log.website && (ex.excludedSince ?? DateTime.MinValue) <= log.date && (ex.excludedTill ?? DateTime.MaxValue) >= log.date)
                            select log;

            //aggregate logs with sum
            var statisticsGroup = from log in validLogs
                                  group log by log.website.ToLower() into g
                                  select new WebsiteStatistics { Date = date.Date, Website = g.Key, TotalVisits = g.Sum(v => v.visits) }
                                      into websiteStatistics
                                  orderby websiteStatistics.TotalVisits descending
                                  select websiteStatistics;

            //execute query at database side
            return statisticsGroup.Take(topX).ToList();
        }
    }
}
