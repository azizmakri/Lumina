using LuminaApp.Application.Commons;
using LuminaApp.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.AttendanceFeatures.Commands.AddAttendance
{
    public class AddAttendanceCommand : IRequest<OperationResult>
    {

        public string StudentId { get; set; }
        public int SessionId { get; set; }
        public string ? observation { get; set; }
        public AttendanceType attendanceType { get; set; }
    }
}
