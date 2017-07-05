using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;
using Manulife.TopFiveWebsites.Service;
using Manulife.TopFiveWebsites.Web.ViewModels;
using Manulife.TopFiveWebsites.Repository;
using Manulife.TopFiveWebsites.Entity;

namespace Manulife.TopFiveWebsites.Web.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WebsiteStatistics, WebsiteStatisticsViewModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.Date));
            CreateMap<ExclusionEntry, VisitLogExclusion>()
                .ForMember(dest => dest.createdBy, opt => opt.UseValue("webappuser"))
                .ForMember(dest => dest.createdOn, opt => opt.UseValue(DateTime.Now));
        }
    }
}