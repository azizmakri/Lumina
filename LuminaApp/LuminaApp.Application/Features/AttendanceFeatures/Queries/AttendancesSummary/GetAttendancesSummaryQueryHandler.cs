using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.AttendanceFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.AttendanceFeatures.Queries.AttendancesSummary
{
    public class GetAttendancesSummaryQueryHandler : IRequestHandler<GetAttendancesSummaryQuery, AttendanceSummeryDTO>
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceService _attendanceService;

        public GetAttendancesSummaryQueryHandler(IMapper mapper, IGenericRepository<Attendance> attendanceRepo, IAttendanceService attendanceService)
        {
            _mapper = mapper;
            _attendanceService = attendanceService;
        }
        public async Task<AttendanceSummeryDTO> Handle(GetAttendancesSummaryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                AttendanceSummeryDTO dTO = new AttendanceSummeryDTO();
                var attendances = await _attendanceService.GetAttendancesByStudentAndSemester(request.StudentId,request.Semester);
                if (attendances != null) {

                    foreach (var attendance in attendances)
                    {
                        dTO.totalHours +=(attendance.session.end_hour.Hour- attendance.session.start_hour.Hour);
                        if (attendance.attendanceType == Domain.Enums.AttendanceType.Absence) {
                            dTO.totalAbsentHours+=(attendance.session.end_hour.Hour-attendance.session.start_hour.Hour);
                        }
                    }
                    return dTO;
                }
                throw new Exception("L'étudiant n'a toujours pas de présence");
            }
            catch (ArgumentException ex)
            {
                // Handle case where student is not found
                throw new NotFoundException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Handle case where evaluations for the student are not available
                throw new NotFoundException(ex.Message);
            }
        }
    }
}
