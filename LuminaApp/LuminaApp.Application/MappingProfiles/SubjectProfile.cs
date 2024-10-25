using AutoMapper;
using LuminaApp.Application.Features.SessionFeatures.Commands.CreateSession;
using LuminaApp.Application.Features.SubjectFeatures.Commands.AddSubject;
using LuminaApp.Application.Features.SubjectFeatures.Dtos;
using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.MappingProfiles
{
    public class SubjectProfile: Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject,subjectDTO>()
                .ForMember(dto => dto.TeacherName, opt => opt.MapFrom(subject => subject.teacher.UserName));

            CreateMap<AddSubjectCommand, Subject>().ReverseMap()
                .ForMember(x => x.teacherId, opt => opt.Ignore())
                .ForMember(x => x.gradeId, opt => opt.Ignore());
        }
    }
}
