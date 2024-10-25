using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuminaGed.Domain.Entities;

namespace LuminaGed.Application.Features.DocumentsFeatures.Dtos
{
    public class DocumentDto
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public DateTime Creation_date { get; set; }
        public string DocumentPath { get; set; }
        public string FolderName { get; set; }
        public string teacherId { get; set; }
        public string studentId { get; set; }
        public string StudentRole { get; set; }
    }
}
