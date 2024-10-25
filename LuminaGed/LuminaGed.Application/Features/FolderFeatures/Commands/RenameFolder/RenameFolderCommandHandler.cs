using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaGed.Application.Commons;
using LuminaGed.Application.Interfaces;
using LuminaGed.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.FolderFeatures.Commands.RenameFolder
{
    public class RenameFolderCommandHandler : IRequestHandler<RenameFolderCommand, OperationResult>
    {
        private readonly IGenericRepository<Folder> _folderRepo;
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;

        public RenameFolderCommandHandler(IGenericRepository<Folder> folderRepo,IFolderService folderService, IMapper mapper)
        {
            _folderRepo = folderRepo;
            _folderService = folderService;
            _mapper = mapper;
        }
        public async Task<OperationResult> Handle(RenameFolderCommand request, CancellationToken cancellationToken)
        {
            try { 
            var folderToUpdate = await _folderRepo.GetByIdAsync(request.FolderId);

            if (folderToUpdate == null)
                throw new NotFoundException("dossier non trouvé");

        // Mettre à jour les propriétés du produit avec les nouvelles valeurs
        folderToUpdate.FolderName = request.NewFolderName;
            

            await _folderRepo.UpdateAsync(folderToUpdate);

                return new OperationResult { Status = true, Message = "Succés." };
            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"erreur lors du mise à jour: {ex.Message}" };
            }
        }
    }
}
