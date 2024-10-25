using LuminaApp.Application.Features.SubjectFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SubjectFeatures.Queries.GetSubjectsByGrade
{
    public record GetSubjectsByGradeQuery:IRequest<ICollection<subjectDTO>>
    {
        public int GradeId { get; set; }

        public GetSubjectsByGradeQuery(int gradeId)
        {
            GradeId = gradeId;
        }
    }
}
