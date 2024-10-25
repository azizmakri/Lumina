using LuminaApp.Application.Features.SessionFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SessionFeatures.Queries.GetByIdStudentAndDate
{
    public class GetSessionsByStudentOnDateQuery : IRequest<List<SessionDTO>>
    {
        public string StudentId { get; }
        public DateTime Date { get; }

        public GetSessionsByStudentOnDateQuery(string studentId, DateTime date)
        {
            StudentId = studentId;
            Date = date;
        }
    }
}
