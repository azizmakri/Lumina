using AutoMapper;
using LuminaApp.Application.Features.AttendanceFeatures.Commands.AddAttendance;
using LuminaApp.Application.Features.AttendanceFeatures.Dtos;
using LuminaApp.Application.Features.AttendanceFeatures.Queries;
using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.MappingProfiles
{
    public class AttendanceProfile:Profile
    {
        public AttendanceProfile()
        {
            CreateMap<Attendance, AttendanceDto>()
                .ForMember(dto => dto.subjectName, opt => opt.MapFrom(src => src.session.subject.SubjectName))
                .ForMember(dto => dto.totalHours, opt => opt.Ignore())
                .ForMember(dto => dto.totalAbsent, opt => opt.Ignore())
                .ForMember(dto => dto.heure_debut, opt => opt.MapFrom(src => src.session.start_hour))
                .ForMember(dto => dto.heure_fin, opt => opt.MapFrom(src => src.session.end_hour));
            CreateMap<Attendance, AttendanceSummeryDTO>()
                .ForMember(dto => dto.totalHours, opt => opt.Ignore())
                .ForMember(dto => dto.totalAbsentHours, opt => opt.Ignore());
            CreateMap<AddAttendanceCommand, Attendance>().ReverseMap()
                .ForMember(command => command.SessionId, opt => opt.Ignore())
                .ForMember(command=>command.StudentId,opt=>opt.Ignore());

        }
    }
}
