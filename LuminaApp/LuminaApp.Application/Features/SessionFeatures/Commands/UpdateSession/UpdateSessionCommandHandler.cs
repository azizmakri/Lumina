using AutoMapper;
using LuminaApp.Application.Commons;
using LuminaApp.Application.Features.SessionFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SessionFeatures.Commands.UpdateSession
{
    public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, OperationResult>
    {
        private readonly ISessionService _sessionService;
        private readonly IGenericRepository<Session> _sessionRepo;
        private readonly IMapper _mapper;

        public UpdateSessionCommandHandler(IGenericRepository<Session> sessionRepo,IMapper mapper, ISessionService sessionService)
        {
            _sessionRepo = sessionRepo;
            _mapper = mapper;
            _sessionService = sessionService;
        }

        public async Task<OperationResult> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sessionToUpdate = await _sessionRepo.GetByIdAsync(request.sessionId);
                if (sessionToUpdate == null)
                {
                    return new OperationResult { Status = false, Message = "Séance non trouvée." };
                }

                // Update only the start and end hour properties
                sessionToUpdate.start_hour = request.start_hour;
                sessionToUpdate.end_hour = request.end_hour;

                await _sessionService.updateSession(sessionToUpdate);

                return new OperationResult { Status = true, Message = "La séance a été mise à jour avec succès." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la mise à jour de la séance : {ex.Message}" };
            }
        }
    }
}

