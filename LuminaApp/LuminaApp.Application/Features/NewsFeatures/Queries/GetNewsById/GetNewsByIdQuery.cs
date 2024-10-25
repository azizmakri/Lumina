using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.NewsFeatures.Queries.GetNewsById
{
    public record GetNewsByIdQuery(int NewsId) : IRequest<byte[]>;
    

    
}
