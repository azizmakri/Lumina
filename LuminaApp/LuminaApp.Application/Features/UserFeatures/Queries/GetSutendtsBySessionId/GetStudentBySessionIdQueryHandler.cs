using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LuminaApp.Application.Features.UserFeatures.Dtos;
using LuminaApp.Application.Features.UserFeatures.Queries.GetChildsFromParent;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;

namespace LuminaApp.Application.Features.UserFeatures.Queries.GetSutendtsBySessionId
{
    public class GetStudentBySessionIdQueryHandler : IRequestHandler<GetStudentBySessionIdQuery, ICollection<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IEvaluationService _evaluationService;
        private readonly IAttendanceService _attendanceService;

        public GetStudentBySessionIdQueryHandler(IMapper mapper, IUserService userService, IEvaluationService evaluationService,IAttendanceService attendanceService)
        {
            _mapper = mapper;
            _userService = userService;
            _evaluationService = evaluationService;
            _attendanceService = attendanceService;
        }

        public async Task<ICollection<UserDto>> Handle(GetStudentBySessionIdQuery request, CancellationToken cancellationToken)
        {
            ICollection<User> students = await _userService.getStudentsByIdSessions(request.sessionId);

            var studentDtos = new List<UserDto>();

            foreach (var student in students)
            {
                var studentDto = _mapper.Map<UserDto>(student);

                var evaluation = await _evaluationService.GetStudentEvaluationBySession(student.Id, request.sessionId);
                if (evaluation != null)
                {
                    studentDto.Note = (float)evaluation.Mark;
                }
                var attendance=await _attendanceService.GetStudentAttendanceBySession(student.Id, request.sessionId);
                if (attendance != null)
                {
                    studentDto.attendanceType = attendance.attendanceType;
                    studentDto.Observation = attendance.Observation;
                }

                studentDtos.Add(studentDto);
            }

            return studentDtos;
        }


    }
}
