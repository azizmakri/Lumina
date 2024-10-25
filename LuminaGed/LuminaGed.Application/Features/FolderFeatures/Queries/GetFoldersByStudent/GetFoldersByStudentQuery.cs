using LuminaGed.Application.Features.FolderFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.FolderFeatures.Queries.GetFoldersByStudent
{
    public record GetFoldersByStudentQuery:IRequest<ICollection<FolderDto>>
    {
        public string StudentId { get; set; }

        public GetFoldersByStudentQuery(string studentId)
        {
            StudentId = studentId;
        }
    }
}
