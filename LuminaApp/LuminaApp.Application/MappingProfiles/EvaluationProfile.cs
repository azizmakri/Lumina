using AutoMapper;
using LuminaApp.Application.Features.EvaluationFeatures.Commands;
using LuminaApp.Application.Features.EvaluationFeatures.Dtos;
using LuminaApp.Application.Features.SessionFeatures.Commands.CreateSession;
using LuminaApp.Domain.Entities;

namespace LuminaApp.Application.MappingProfiles
{
    public class EvaluationProfile:Profile
    {
        public EvaluationProfile()
        {
            CreateMap<Evaluation, EvaluationDTO>()
                .ForMember(dto=>dto.SubjectName,opt=>opt.MapFrom(ec=>ec.session.subject.SubjectName))
                .ForMember(dto=>dto.EvaluationDate,opt=>opt.MapFrom(ev=>ev.session.start_hour))
                .ForMember(dto=>dto.coefficient, opt=>opt.MapFrom(ev=>ev.session.subject.coefficient))
                .ForMember(dto=>dto.ClassHeighestMark,opt=>opt.Ignore())
                .ForMember(dto=>dto.ClassLowestMark,opt=>opt.Ignore())
                .ForMember(dto=>dto.ClassAverage,opt=>opt.Ignore());
            CreateMap<CreateEvaluationCommand, Evaluation>().ReverseMap()
                .ForMember(x => x.sessionId, opt => opt.Ignore())
                .ForMember(x => x.StudentId, opt => opt.Ignore());
            CreateMap<Evaluation, BulletinDTO>()
                .ForMember(dto => dto.SubjectName, opt => opt.MapFrom(ec => ec.session.subject.SubjectName))
                .ForMember(dto => dto.coef, opt => opt.MapFrom(ev => ev.session.subject.coefficient))
                .ForMember(dto => dto.Result, opt => opt.Ignore())
                .ForMember(dto => dto.ControleResult, opt => opt.Ignore())
                .ForMember(dto => dto.SyntheseResult, opt => opt.Ignore())
                .ForMember(dto => dto.Teacher, opt => opt.Ignore())
                .ForMember(dto => dto.OraleResult, opt => opt.Ignore());
        }
    }
}
