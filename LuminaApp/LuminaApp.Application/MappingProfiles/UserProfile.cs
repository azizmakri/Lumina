using AutoMapper;
using LuminaApp.Application.Features.UserFeatures.Dtos;
using LuminaApp.Domain.Entities;

namespace LuminaApp.Application.MappingProfiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, ChildDTO>();
            CreateMap<User, UserDto>()
                .ForMember(x => x.role, opt => opt.Ignore())
                .ForMember(x => x.grade, opt => opt.MapFrom(u=> u.grade.Level+u.grade.Name));
        }
    }
}
