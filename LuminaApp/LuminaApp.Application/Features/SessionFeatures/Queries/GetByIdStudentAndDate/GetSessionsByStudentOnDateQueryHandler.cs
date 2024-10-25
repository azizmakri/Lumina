using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.EvaluationFeatures.Dtos;
using LuminaApp.Application.Features.SessionFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SessionFeatures.Queries.GetByIdStudentAndDate
{
    public class GetSessionsByStudentOnDateQueryHandler : IRequestHandler<GetSessionsByStudentOnDateQuery, List<SessionDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;

        public GetSessionsByStudentOnDateQueryHandler(IMapper mapper, ISessionService sessionService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
        }
        public async Task<List<SessionDTO>> Handle(GetSessionsByStudentOnDateQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var sessions = await _sessionService.GetSessionsByStudentAndAate(request.StudentId, request.Date);
                var sessionsDto = _mapper.Map<List<SessionDTO>>(sessions);

                return sessionsDto;
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
