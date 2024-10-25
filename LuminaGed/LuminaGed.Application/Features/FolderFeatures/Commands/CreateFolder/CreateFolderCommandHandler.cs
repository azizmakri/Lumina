using AutoMapper;
using LuminaGed.Application.Commons;
using LuminaGed.Application.Interfaces;
using LuminaGed.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.FolderFeatures.Commands.CreateFolder
{
    public class CreateFolderCommandHandler : IRequestHandler<CreateFolderCommand, OperationResult>
    {
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;

        public CreateFolderCommandHandler(IMapper mapper, IFolderService folderService)
        {
            _mapper = mapper;
            _folderService = folderService;
        }

        public async Task<OperationResult> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
        {
            var folderToCreate = _mapper.Map<Folder>(request);

            try
            {
                await _folderService.AddFolder(folderToCreate, request.TeacherId, request.ParenFolderId,request.gradeId);
                return new OperationResult { Status = true, Message = "Dossier créé avec succès"};

            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la création du dossier: {ex.Message}" };
            }
        }
    }
}
    

