using LuminaApp.Application.Features.GradeFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.GradeFeatures.Queries.GetGradesByTeacher
{
    public record GetGradesByTeacherQuery:IRequest<ICollection<GradeDto>>
    {
        public string TeacherId { get; set; }

        public GetGradesByTeacherQuery(string teacherId)
        {
            TeacherId = teacherId;
        }
    }
}
