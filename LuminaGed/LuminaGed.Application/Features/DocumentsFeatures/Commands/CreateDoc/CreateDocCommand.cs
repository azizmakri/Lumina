using LuminaGed.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.DocumentsFeatures.Commands.CreateDoc
{
    public record CreateDocCommand:IRequest<OperationResult>
    {
        public string DocumentName { get; set; }
        public DateTime Creation_date { get; set; }
        public string DocumentPath { get; set; }
        public string TeacherId { get; set; }
        public int folderId { get; set; }
    }
}
