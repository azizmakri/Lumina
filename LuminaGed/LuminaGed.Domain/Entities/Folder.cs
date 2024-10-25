using LuminaGed.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Domain.Entities
{
    public class Folder
    {
        [Key]
        public int FolderId { get; set; }

        public string FolderName { get; set; }
        public DateTime Creation_Date { get; set; }
        public DateTime Modification_Date { get; set; }
        public FolderType folderType { get; set; }

        public int? GradeFK { get; set; }
        public virtual ICollection<DocumentEntity>? Documents { get; set; }
        public virtual User? Teacher { get; set; }
        public string? TeacherFK { get; set; }
        public int? ParenFolderFK { get; set; }
        public virtual ICollection<Folder>? Folders { get; set; }
        public virtual Folder? ParentFolder { get; set; }
        public virtual Grade? grade { get; set; }
    }
}
