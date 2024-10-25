using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.SessionFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SessionFeatures.Queries.GetSessionById
{
    public class GetSessionByIdQueryHandler : IRequestHandler<GetSessionByIdQuery, SessionDTO>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;

        public GetSessionByIdQueryHandler(IMapper mapper,ISessionService sessionService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
        }
        async Task<SessionDTO> IRequestHandler<GetSessionByIdQuery, SessionDTO>.Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var session = await _sessionService.GetSessionsById(request.SessionId);
                var sessionDto = _mapper.Map<SessionDTO>(session);


                return sessionDto;
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
