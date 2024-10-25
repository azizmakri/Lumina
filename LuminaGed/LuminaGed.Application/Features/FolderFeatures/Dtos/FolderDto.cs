using LuminaGed.Application.Features.DocumentsFeatures.Dtos;
using LuminaGed.Domain.Enums;

namespace LuminaGed.Application.Features.FolderFeatures.Dtos
{
    public class FolderDto
    {
        public int FolderId { get; set; }
        public string FolderName { get; set; }
        public DateTime Creation_Date { get; set; }
        public DateTime Modification_Date { get; set; }
        public ICollection<ChildFolcerDTO> folders { get; set; }
        public ICollection<DocumentDto>? Documents { get; set; }
        public string teacherId { get; set; }
        public FolderType folderType { get; set; }
        public int gradeId { get; set; }

        public string FolderTypeName => folderType switch
        {
            FolderType.Course => "cours",
            FolderType.ToDo => "devoirs",
            FolderType.Other => "autre",
            _ => "unknown"
        };

    }
}
