using LuminaApp.Application.Features.EvaluationFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.EvaluationFeatures.Queries.GetEvaluationByStudent
{
    public class GetEvaByStudentQuery : IRequest<ICollection<EvaluationDTO>>
    {
        public string StudentId { get; set; }

        public GetEvaByStudentQuery(string studentId)
        {
            StudentId = studentId;
        }
    }
}
