using LuminaGed.Application.Commons;
using LuminaGed.Domain.Enums;
using MediatR;

namespace LuminaGed.Application.Features.FolderFeatures.Commands.CreateFolder
{
    public record CreateFolderCommand: IRequest<OperationResult>
    {
        public string FolderName { get; set; }
        public DateTime Creation_Date { get; set; }
        public FolderType folderType { get; set; }
        public string TeacherId { get; set; }
        public int ParenFolderId { get; set; }
        public int gradeId { get; set; }
      
    }
}
