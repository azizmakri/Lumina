using LuminaGed.Application.Features.DocumentsFeatures.Dtos;
using LuminaGed.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.FolderFeatures.Dtos
{
    public record ChildFolcerDTO
    {
        public int FolderId { get; set; }
        public string FolderName { get; set; }
        public DateTime Creation_Date { get; set; }
        public DateTime Modification_Date { get; set; }
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
