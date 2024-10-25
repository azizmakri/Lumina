using LuminaGed.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.FolderFeatures.Commands.DeleteFolder
{
    public record DeleteFolderCommand:IRequest<OperationResult>
    {
        public int FolderId { get; set; }
        public string OwnerId { get; set; }
    }
}
