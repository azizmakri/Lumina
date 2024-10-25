using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuminaGed.Application.Commons;
using MediatR;

namespace LuminaGed.Application.Features.DocumentsFeatures.Commands.DeleteDoc
{
    public record DeleteDocCommand : IRequest<OperationResult>
    {
        public int DocumentId { get; set; }

    }
}

