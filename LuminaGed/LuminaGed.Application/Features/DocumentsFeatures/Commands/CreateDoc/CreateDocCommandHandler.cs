using AutoMapper;
using LuminaGed.Application.Commons;
using LuminaGed.Application.Features.FolderFeatures.Commands.CreateFolder;
using LuminaGed.Application.Interfaces;
using LuminaGed.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.DocumentsFeatures.Commands.CreateDoc
{
    public record class CreateDocCommandHandler : IRequestHandler<CreateDocCommand, OperationResult>
    {
        private readonly IDocService _docService;
        private readonly IMapper _mapper;

        public CreateDocCommandHandler(IMapper mapper, IDocService docService)
        {
            _mapper = mapper;
            _docService = docService;
        }

        public async Task<OperationResult> Handle(CreateDocCommand request, CancellationToken cancellationToken)
        {
            var docToCreate = _mapper.Map<DocumentEntity>(request);

            try
            {
                await _docService.AddDoc(docToCreate, request.TeacherId, request.folderId);
                return new OperationResult { Status = true, Message = "Fichier créé avec succès" };

            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la création du fichier: {ex.Message}" };
            }
        }
    }
}
