using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuminaGed.Application.Features.DocumentsFeatures.Dtos;
using MediatR;

namespace LuminaGed.Application.Features.DocumentsFeatures.Queries.AllDocuments
{
    public record GetAllDocQuery : IRequest<List<DocumentDto>>
    {
    }

}
