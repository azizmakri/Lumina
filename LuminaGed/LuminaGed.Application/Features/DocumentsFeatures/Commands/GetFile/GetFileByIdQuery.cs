using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.DocumentsFeatures.Commands.GetFile
{
    public record GetFileByIdQuery(int DocumentId) : IRequest<byte[]>;

}
