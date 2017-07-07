using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;
using Manulife.TopFiveWebsites.Service;
using Manulife.TopFiveWebsites.Web.ViewModels;
using Manulife.TopFiveWebsites.Entity;
using Manulife.TopFiveWebsites.Service.Interface;
using Manulife.TopFiveWebsites.Repository.Interface;

namespace Manulife.TopFiveWebsites.Web.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WebsiteStatistics, WebsiteStatisticsViewModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.Date));
            CreateMap<ExclusionEntry, VisitLogExclusion>()
                .ForMember(dest => dest.createdBy, opt => opt.MapFrom(src => HttpContext.Current.User.Identity.Name))
                .ForMember(dest => dest.createdOn, opt => opt.UseValue(DateTime.Now));
            CreateMap<UserProfile, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.password))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.roles));
        }
    }
}