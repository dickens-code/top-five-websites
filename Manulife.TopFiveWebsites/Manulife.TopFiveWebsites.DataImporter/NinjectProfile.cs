using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ninject.Modules;
using Manulife.TopFiveWebsites.Entity;
using Manulife.TopFiveWebsites.Service;
using Manulife.TopFiveWebsites.Repository;
using Manulife.TopFiveWebsites.Service.Interface;
using Manulife.TopFiveWebsites.Repository.Interface;

namespace Manulife.TopFiveWebsites.DataImporter
{
    public class NinjectProfile : NinjectModule
    {
        public override void Load()
        {
            Kernel.Settings.AllowNullInjection = true;
            Bind<TopFiveWebsitesEntities>().ToSelf().InSingletonScope();
            Bind<ISearchService>().To<SearchService>().InSingletonScope();
            Bind<IVisitLogService>().To<VisitLogService>().InSingletonScope();
            Bind<IDataStoreRepository>().To<DataStoreRepository>().InSingletonScope();
            Bind<IExclusionEntryRepository>().ToConstant<IExclusionEntryRepository>(null).InSingletonScope();
        }
    }
}