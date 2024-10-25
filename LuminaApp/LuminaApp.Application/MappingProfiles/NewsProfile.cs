using AutoMapper;
using LuminaApp.Application.Features.NewsFeatures.Commands;
using LuminaApp.Application.Features.NewsFeatures.Dtos;
using LuminaApp.Domain.Entities;

namespace LuminaApp.Application.MappingProfiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<CreateNewsCommand, SchoolNews>().ReverseMap()
                .ForMember(x => x.AdminId, opt => opt.Ignore());
            CreateMap<SchoolNews, NewsDTO>();

        }
    }
}
