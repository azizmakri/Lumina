using LuminaApp.Application.Features.EvaluationFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.EvaluationFeatures.Queries.GetBulletinByStudent
{
    public record GetBulletinByStudentQuery : IRequest<ICollection<BulletinDTO>>
    {
        public string StudentId { get; set; }
        public int Semester { get; set; }

        public GetBulletinByStudentQuery(string studentId, int semester)
        {
            StudentId = studentId;
            Semester = semester;
        }
    }
}

