using AutoMapper;
using LuminaApp.Application.Features.ClassRoomFeatures.Commands.CreateClassroom;
using LuminaApp.Application.Features.ClassRoomFeatures.Dtos;
using LuminaApp.Application.Features.SubjectFeatures.Commands.AddSubject;
using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.MappingProfiles
{
    public class ClassRoomProfile:Profile
    {
        public ClassRoomProfile()
        {
            CreateMap<ClassRoom,ClassRoomDTO>();

            CreateMap<CreateClassroomCommand, ClassRoom>().ReverseMap();
            /*  .ForMember(x => x.adminId, opt => opt.Ignore());*/




        }
    }
}
