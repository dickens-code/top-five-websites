using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Manulife.TopFiveWebsites.Entity;
using Manulife.TopFiveWebsites.Repository;

namespace Manulife.TopFiveWebsites.Service
{
    public class VisitLogService : IVisitLogService
    {
        protected readonly IDataStoreRepository _dataStoreRepository;
        protected readonly IExclusionEntryRepository _exclusionEntryRepository;

        public VisitLogService(IExclusionEntryRepository exclusionEntryRepository, IDataStoreRepository dataStoreRepository)
        {
            _exclusionEntryRepository = exclusionEntryRepository;
            _dataStoreRepository = dataStoreRepository;
        }

        public void PersistExclusionEntries()
        {
            //get exclusion fro restful resource
            var exclusionEntries = _exclusionEntryRepository.GetEntities<ExclusionEntry>();

            var visitLogEntities = Mapper.Map<IList<ExclusionEntry>, IList<VisitLogExclusion>>(exclusionEntries.ToList());

            //add exclusion to db
            foreach (var visitLogEntry in visitLogEntities)
                _dataStoreRepository.AddEntity(visitLogEntry);

            //clear exclusion records in table
            _dataStoreRepository.TruncateStore<VisitLogExclusion>();

            _dataStoreRepository.SaveChanges();
        }
    }
}
