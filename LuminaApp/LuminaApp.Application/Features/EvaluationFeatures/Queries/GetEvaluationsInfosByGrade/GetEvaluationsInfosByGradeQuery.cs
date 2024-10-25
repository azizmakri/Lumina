using LuminaApp.Application.Features.EvaluationFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.EvaluationFeatures.Queries.GetEvaluationsInfosByGrade
{
    public record GetEvaluationsInfosByGradeQuery:IRequest<ICollection<EvaluationDTO>>
    {
        public string StudentId { get; set; }

        public GetEvaluationsInfosByGradeQuery(string studentId)
        {
            StudentId = studentId;
        }
    }
}
