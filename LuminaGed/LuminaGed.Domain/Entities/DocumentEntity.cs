using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Domain.Entities
{
    public class DocumentEntity
    {
        [Key]
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public DateTime Creation_date { get; set; }
        public string DocumentPath { get; set; }
        public int? folderFK { get; set; }
        public string? studentFK { get; set; }
        public string? teacherFK { get; set; }
        public virtual Folder? folder { get; set; }
        public virtual User? student { get; set; }
        public virtual User? teacher { get; set; }
    }
}
