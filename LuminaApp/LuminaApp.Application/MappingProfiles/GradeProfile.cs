using AutoMapper;
using LuminaApp.Application.Features.GradeFeatures.Commands.CreateGrade;
using LuminaApp.Application.Features.GradeFeatures.Dtos;
using LuminaApp.Application.Features.SessionFeatures.Commands.CreateSession;
using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.MappingProfiles
{
    public class GradeProfile:Profile
    {
        public GradeProfile()
        {
            CreateMap<CreateGradeCommand, Grade>().ReverseMap()
                .ForMember(dto=>dto.studentIds,opt=>opt.Ignore());
            CreateMap<Grade, GradeDto>();
        }
    }
}
