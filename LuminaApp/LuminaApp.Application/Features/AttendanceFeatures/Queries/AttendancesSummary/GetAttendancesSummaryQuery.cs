using LuminaApp.Application.Features.AttendanceFeatures.Dtos;
using MediatR;

namespace LuminaApp.Application.Features.AttendanceFeatures.Queries.AttendancesSummary
{
    public record GetAttendancesSummaryQuery:IRequest<AttendanceSummeryDTO>
    {
        public string StudentId { get; set; }
        public int Semester { get; set; }

        public GetAttendancesSummaryQuery(string studentId,int semester)
        {
            StudentId = studentId;
            Semester = semester;
        }
    }
}
