using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using AutoMapper;
using Manulife.TopFiveWebsites.Entity;
using CsvHelper;
using CsvHelper.Configuration;
using Manulife.TopFiveWebsites.Repository.Interface;
using Manulife.TopFiveWebsites.Service.Interface;

namespace Manulife.TopFiveWebsites.Service
{
    public class VisitLogService : IVisitLogService
    {
        protected readonly IDataStoreRepository _dataStoreRepository;
        protected readonly IExclusionEntryRepository _exclusionEntryRepository;
        //4 hr time-out on exclusion list retrieved from resetful api
        protected TimeSpan _visitLogExclusionLifetime = new TimeSpan(4, 0, 0);

        public VisitLogService(IExclusionEntryRepository exclusionEntryRepository, IDataStoreRepository dataStoreRepository)
        {
            _exclusionEntryRepository = exclusionEntryRepository;
            _dataStoreRepository = dataStoreRepository;
        }

        public void PersistExclusionEntries(bool forceRefresh)
        {
            if(! forceRefresh)
            {
                //get latest refresh time for exclusion entry
                var lastRefreshDatetime = _dataStoreRepository.GetEntities<VisitLogExclusion>()
                    .OrderByDescending(e => e.createdOn)
                    .Select(e => e.createdOn)
                    .FirstOrDefault();

                //if refresh within 4 hr before, do nothing to prevent too many restful resource traffic
                if (DateTime.Now.Subtract(lastRefreshDatetime) < _visitLogExclusionLifetime)
                    return;
            }

            //get exclusion entry from restful resource
            var exclusionEntries = _exclusionEntryRepository.GetEntities<ExclusionEntry>();

            var visitLogEntities = Mapper.Map<IList<ExclusionEntry>, IList<VisitLogExclusion>>(exclusionEntries.ToList());

            //add exclusion to db
            foreach (var visitLogEntry in visitLogEntities)
                _dataStoreRepository.AddEntity(visitLogEntry);

            //clear exclusion records in table
            _dataStoreRepository.TruncateStore<VisitLogExclusion>();

            _dataStoreRepository.SaveChanges();
        }

        public int ImportVisitLogSource(TextReader reader)
        {
            var csvReader = new CsvReader(reader, new CsvConfiguration { Delimiter = "|" });

            //for large csv file, read row by row to prevent memory peak
            while(csvReader.Read())
            {
                var visitLog = new VisitLog
                {
                    date = csvReader.GetField<DateTime>(nameof(VisitLog.date)),
                    website = csvReader.GetField<string>(nameof(VisitLog.website)),
                    visits = csvReader.GetField<int>(nameof(VisitLog.visits)),
                    createdBy = "dataImporter",
                    createdOn = DateTime.Now
                };
                _dataStoreRepository.AddEntity(visitLog);
            }

            return _dataStoreRepository.SaveChanges();
        }

        public void ResetVisitLog()
        {
            throw new NotImplementedException("suppose ResetVisitLog() is not coded!!!");
            //_dataStoreRepository.TruncateStore<VisitLog>();
        }

        public void ResetVisitLogExclusion()
        {
            _dataStoreRepository.TruncateStore<VisitLogExclusion>();
        }
    }
}
