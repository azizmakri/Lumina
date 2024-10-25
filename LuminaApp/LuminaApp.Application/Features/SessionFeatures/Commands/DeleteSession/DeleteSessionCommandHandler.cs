using LuminaApp.Application.Commons;
using LuminaApp.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SessionFeatures.Commands.DeleteSession
{
    public class DeleteSessionCommandHandler:IRequestHandler<DeleteSessionCommand,OperationResult>
    {
        private readonly ISessionService _sessionService;

        public DeleteSessionCommandHandler(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<OperationResult> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _sessionService.DeleteSession(request.SessionId);
                return new OperationResult { Status = true, Message = "Séance supprimée avec succès" };
            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la suppression de la séance: {ex.Message}" };
            }

        }
    }
}
