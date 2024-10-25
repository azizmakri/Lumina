using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using LuminaGed.Application.Commons;
using LuminaGed.Application.Interfaces;

namespace LuminaGed.Application.Features.DocumentsFeatures.Commands.DeleteDoc
{
    public class DeleteDocCommandHandler : IRequestHandler<DeleteDocCommand, OperationResult>
    {
        private readonly IDocService _docService;

        public DeleteDocCommandHandler(IDocService docService)
        {
            _docService = docService;
        }

        public async Task<OperationResult> Handle(DeleteDocCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Call your service method to delete the document by ID
                await _docService.DeleteDoc(request.DocumentId);
                return new OperationResult { Status = true, Message = "Document supprimé avec succès" };
            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la suppression du fichier: {ex.Message}" };
            }
        }
    }
}
