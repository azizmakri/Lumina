using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.SessionFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using MediatR;

namespace LuminaApp.Application.Features.SessionFeatures.Queries.GetAllSeesionsByTeacherId
{
    public class GetSessionsByTeachIdQueryHandler : IRequestHandler<GetAllSessionsByTeachIdQuery, List<SessionDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;

        public GetSessionsByTeachIdQueryHandler(IMapper mapper, ISessionService sessionService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
        }

        public async Task<List<SessionDTO>> Handle(GetAllSessionsByTeachIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sessions = await _sessionService.GetSessionsByTeacherId(request.TeacherId);
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
