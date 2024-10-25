using AutoMapper;
using LuminaApp.Application.Features.SessionFeatures.Commands.CreateSession;
using LuminaApp.Application.Features.SessionFeatures.Commands.UpdateSession;
using LuminaApp.Application.Features.SessionFeatures.Dtos;
using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.MappingProfiles
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<CreateSessionCommand, Session>().ReverseMap();
            CreateMap<Session, SessionDTO>()
                .ForMember(dto => dto.subjectName, opt => opt.MapFrom(session => session.subject.SubjectName))
                .ForMember(dto => dto.gradeId, opt => opt.MapFrom(session => session.subject.grade.GradeId))
                .ForMember(dto => dto.gradeName, opt => opt.MapFrom(session => session.subject.grade.Level+session.subject.grade.Name))
                .ForMember(dto => dto.classroomName, opt => opt.MapFrom(session => session.ClassRoom.ClassroomName));
            CreateMap<UpdateSessionCommand, Session>();
        }
    }
}
