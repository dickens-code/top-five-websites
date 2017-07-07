using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ninject.Modules;
using Ninject.Web.Common;
using RestSharp;

using Manulife.TopFiveWebsites.Entity;
using Manulife.TopFiveWebsites.Service;
using Manulife.TopFiveWebsites.Repository;
using Manulife.TopFiveWebsites.Service.Interface;
using Manulife.TopFiveWebsites.Repository.Interface;

namespace Manulife.TopFiveWebsites.Web.App_Start
{
    public class NinjectProfile : NinjectModule
    {
        public override void Load()
        {
            Bind<TopFiveWebsitesEntities>().ToSelf().InRequestScope();
            Bind<ISearchService>().To<SearchService>().InRequestScope();
            Bind<IVisitLogService>().To<VisitLogService>().InRequestScope();
            Bind<IDataStoreRepository>().To<DataStoreRepository>().InRequestScope();
            Bind<IExclusionEntryRepository>().To<ExclusionEntryRepository>().InRequestScope();
            Bind<IRestClient>().To<RestClient>().InRequestScope();
            Bind<ILoginService>().To<LoginService>().InRequestScope();
        }
    }
}