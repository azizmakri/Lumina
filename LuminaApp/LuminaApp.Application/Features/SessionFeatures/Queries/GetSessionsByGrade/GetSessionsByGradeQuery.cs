using LuminaApp.Application.Features.SessionFeatures.Dtos;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SessionFeatures.Queries.GetSessionsByGrade
{
    public class GetSessionsByGradeQuery:IRequest<List<SessionDTO>>
    {
        public int gradeId { get; set; }

        public GetSessionsByGradeQuery(int gradeId)
        {
            this.gradeId = gradeId;
        }
    }
}
