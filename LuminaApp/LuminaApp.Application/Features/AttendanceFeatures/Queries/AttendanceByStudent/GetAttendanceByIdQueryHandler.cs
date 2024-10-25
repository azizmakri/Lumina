

using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.AttendanceFeatures.Dtos;
using LuminaApp.Application.Features.EvaluationFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.AttendanceFeatures.Queries.AttendanceByStudent
{
    public class GetAttendanceByIdQueryHandler : IRequestHandler<GetAttendanceByIdQuery, ICollection<AttendanceDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceService _attendanceService;

        public GetAttendanceByIdQueryHandler(IMapper mapper, IGenericRepository<Attendance> attendanceRepo, IAttendanceService attendanceService)
        {
            _mapper = mapper;
            _attendanceService = attendanceService;
        }


        async Task<ICollection<AttendanceDto>> IRequestHandler<GetAttendanceByIdQuery, ICollection<AttendanceDto>>.Handle(GetAttendanceByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var attendances = await _attendanceService.GetAttendancesByStudentAndSemester(request.StudentId,request.Semester);

                var AttendanceDto = _mapper.Map<ICollection<AttendanceDto>>(attendances);
                foreach ( var attendancedto in AttendanceDto)
                {
                    foreach (var attendance in attendances) {
                        if (attendance.session.subject.SubjectName ==attendancedto.subjectName)
                        {
                            attendancedto.totalHours +=(attendance.session.end_hour.Hour - attendance.session.start_hour.Hour);
                            if (attendance.attendanceType == Domain.Enums.AttendanceType.Absence)
                            {
                                attendancedto.totalAbsent +=(attendance.session.end_hour.Hour - attendance.session.start_hour.Hour);
                            }
                        }
                    }
                    
                }

                return AttendanceDto;
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
