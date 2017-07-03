using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;
using Manulife.TopFiveWebsites.Service;
using Manulife.TopFiveWebsites.Web.ViewModels;

namespace Manulife.TopFiveWebsites.Web.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WebsiteStatistics, WebsiteStatisticsViewModel>()
                .ForMember(vm => vm.Date, opt => opt.MapFrom(x => x.Date.Date))
                .ForMember(vm => vm.Website, opt => opt.MapFrom(x => x.Website))
                .ForMember(vm => vm.TotalVisits, opt => opt.MapFrom(x => x.TotalVisits));
        }
    }
}