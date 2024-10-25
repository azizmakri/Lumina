using AutoMapper;
using LuminaGed.Application.Features.FolderFeatures.Commands.CreateFolder;
using LuminaGed.Application.Features.FolderFeatures.Commands.RenameFolder;
using LuminaGed.Application.Features.FolderFeatures.Dtos;
using LuminaGed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.MappingProfiles
{
    public class FolderProfile: Profile

    {
        public FolderProfile()
        {
            CreateMap<CreateFolderCommand, Folder>().ReverseMap()
                .ForMember(x => x.TeacherId, opt => opt.Ignore())
                .ForMember(x => x.gradeId, opt => opt.Ignore())
                .ForMember(x => x.ParenFolderId, opt => opt.Ignore());

            CreateMap<RenameFolderCommand, Folder>().ReverseMap();

            CreateMap<Folder, FolderDto>()
                .ForMember(x => x.teacherId, opt => opt.MapFrom(y => y.TeacherFK))
                .ForMember(x => x.gradeId, opt => opt.MapFrom(y => y.grade.GradeId))
                ;
            CreateMap<Folder, ChildFolcerDTO>()
                .ForMember(x => x.teacherId, opt => opt.MapFrom(y => y.TeacherFK))
                .ForMember(x => x.gradeId, opt => opt.MapFrom(y => y.grade.GradeId))
                ;

        }
    }

    }
