using AutoMapper;
using LuminaGed.Application.Commons;
using LuminaGed.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.FolderFeatures.Commands.DeleteFolder
{
    public class DeleteFolderCommandHandler : IRequestHandler<DeleteFolderCommand, OperationResult>
    {
        private readonly IFolderService _folderService;

        public DeleteFolderCommandHandler(IFolderService folderService)
        {
            _folderService = folderService;
        }
        public async Task<OperationResult> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Call your service method to delete the document by ID
                await _folderService.DeleteFolder(request.FolderId,request.OwnerId);
                return new OperationResult { Status = true, Message = "Document deleted successfully" };
            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Failed to delete document: {ex.Message}" };
            }
        }
    }
}
