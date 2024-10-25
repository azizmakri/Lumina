using AutoMapper;
using LuminaApp.Application.Features.NotificationFeatures.Commands;
using LuminaApp.Application.Features.NotificationFeatures.Dtos;
using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.MappingProfiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationEntity, NotificationDTO>()
                .ForMember(dest => dest.read, opt => opt.MapFrom(src => src.read));
            CreateMap<UpdateNotificationCommand, NotificationEntity>();
        }
    }
}
