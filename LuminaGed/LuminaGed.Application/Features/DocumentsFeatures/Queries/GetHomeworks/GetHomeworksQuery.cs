using LuminaGed.Application.Features.DocumentsFeatures.Dtos;
using LuminaGed.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.DocumentsFeatures.Queries.GetHomeworks
{
    public record GetHomeworksQuery:IRequest<ICollection<HomeworksDto>>
    {
        public string StudentId { get; set; }

        public GetHomeworksQuery(string studentId)
        {
            StudentId = studentId;
        }
    }
}
