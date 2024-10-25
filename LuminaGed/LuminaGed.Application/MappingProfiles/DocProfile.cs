using AutoMapper;
using LuminaGed.Application.Features.DocumentsFeatures.Commands.CreateDoc;
using LuminaGed.Application.Features.DocumentsFeatures.Dtos;
using LuminaGed.Application.Features.FolderFeatures.Commands.CreateFolder;
using LuminaGed.Application.Features.FolderFeatures.Dtos;
using LuminaGed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.MappingProfiles
{
    public class DocProfile: Profile
    {
        public DocProfile()
        {
            CreateMap<CreateDocCommand, DocumentEntity>().ReverseMap()
                .ForMember(x => x.TeacherId, opt => opt.Ignore())
                .ForMember(x => x.folderId, opt => opt.Ignore());

            CreateMap<DocumentEntity, DocumentDto>()
                .ForMember(x=>x.FolderName, opt => opt.MapFrom(y=>y.folder.FolderName))
                .ForMember(x=>x.teacherId, opt => opt.MapFrom(y=>y.teacherFK))
                .ForMember(x=>x.studentId, opt => opt.MapFrom(y=>y.studentFK))
                ;
            CreateMap<DocumentEntity, HomeworksDto>()
                .ForMember(x => x.FolderId, opt => opt.MapFrom(y => y.folder.FolderId))
                .ForMember(x => x.GradeId, opt => opt.MapFrom(y => y.folder.grade.GradeId));

        }
    }
}
