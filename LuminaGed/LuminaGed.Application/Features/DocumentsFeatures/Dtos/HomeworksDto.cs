using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.DocumentsFeatures.Dtos
{
    public record HomeworksDto
    {
        public int DocumentId { get; set; }
        public int FolderId { get; set; }
        public int GradeId { get; set; }
        public string DocumentName { get; set; }
        public DateTime Creation_date { get; set; }
        public string DocumentPath { get; set; }
        public string status { get; set; }
    }
}
