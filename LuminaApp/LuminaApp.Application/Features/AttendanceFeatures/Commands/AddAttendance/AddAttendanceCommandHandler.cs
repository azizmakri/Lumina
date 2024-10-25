using AutoMapper;
using LuminaApp.Application.Commons;
using LuminaApp.Application.Features.AttendanceFeatures.Commands.AddAttendance;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.AttendanceFeatures.Commands.AddAttendance
{
    public class AddAttendanceCommandHandler : IRequestHandler<AddAttendanceCommand, OperationResult>
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IMapper _mapper;

        public AddAttendanceCommandHandler(IAttendanceService attendanceService,IMapper mapper)
        {
            _attendanceService = attendanceService;
            _mapper = mapper;
        }

        public async Task<OperationResult> Handle(AddAttendanceCommand request, CancellationToken cancellationToken)
        {
            var AttendenceToCreate = _mapper.Map<Attendance>(request);
            try
            {
                await _attendanceService.AddAttendance(AttendenceToCreate,request.StudentId,request.SessionId);
                return new OperationResult { Status = true, Message = "Présence créée avec succès" };
            }
            catch(Exception ex) {
                return new OperationResult { Status = false, Message = $"Erreur lors de la création d'une présence: {ex.Message}" };
            }
        }
    }
}
