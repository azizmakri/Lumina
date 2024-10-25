using LuminaGed.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.FolderFeatures.Commands.RenameFolder
{
    public class RenameFolderCommand : IRequest<OperationResult>
    {
        public int FolderId { get; set; }        
        public string NewFolderName { get; set; } 
                   
      

    }
}
