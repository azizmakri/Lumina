using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.SessionFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;

namespace LuminaApp.Application.Features.SessionFeatures.Queries.GetSessionsByGrade
{
    public class GetSessionsByGradeHandler : IRequestHandler<GetSessionsByGradeQuery, List<SessionDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ISessionService _sessionService;
        public GetSessionsByGradeHandler(IMapper mapper, ISessionService sessionService)
        {
            _mapper = mapper;
            _sessionService = sessionService;
        }

        public async Task<List<SessionDTO>> Handle(GetSessionsByGradeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sessions = await _sessionService.GetSessionsByGrade(request.gradeId);
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
            throw new NotImplementedException();
        }
    }
}
