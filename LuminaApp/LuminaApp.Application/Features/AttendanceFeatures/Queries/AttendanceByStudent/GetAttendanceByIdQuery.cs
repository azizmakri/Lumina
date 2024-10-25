using LuminaApp.Application.Features.AttendanceFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.AttendanceFeatures.Queries.AttendanceByStudent
{
    public class GetAttendanceByIdQuery : IRequest<ICollection<AttendanceDto>>
    {

        public string StudentId { get; set; }
        public int Semester { get; set; }

        public GetAttendanceByIdQuery(string studentId, int semester)
        {
            StudentId = studentId;
            Semester = semester;
        }

    }
}
