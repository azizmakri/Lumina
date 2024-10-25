using LuminaGed.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.DocumentsFeatures.Commands.GetFile
{
    public class GetFileByIdQueryHandler : IRequestHandler<GetFileByIdQuery, byte[]>
    {
        private readonly IDocService _docService;

        public GetFileByIdQueryHandler(IDocService docService)
        {
            _docService = docService;
        }

        public async Task<byte[]> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _docService.GetFileByIdAsync(request.DocumentId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Échec de la récupération du fichier: {ex.Message}");
            }
        }
    }
}
