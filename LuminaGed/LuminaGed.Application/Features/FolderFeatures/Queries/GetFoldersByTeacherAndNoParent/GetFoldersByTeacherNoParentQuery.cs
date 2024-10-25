using LuminaGed.Application.Features.FolderFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.FolderFeatures.Queries.GetFoldersByTeacherAndNoParent
{
    public record GetFoldersByTeacherNoParentQuery:IRequest<ICollection<FolderDto>>
    {
        public string TeacherId { get; set; }
        public int GradeId { get; set; }

        public GetFoldersByTeacherNoParentQuery(string teacherId, int gradeId)
        {
            TeacherId = teacherId;
            GradeId = gradeId;
        }
    }
}
