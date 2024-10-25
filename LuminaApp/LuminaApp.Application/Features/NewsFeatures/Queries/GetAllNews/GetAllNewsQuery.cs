using LuminaApp.Application.Features.NewsFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.NewsFeatures.Queries.GetAllNews
{
    public record GetAllNewsQuery : IRequest<List<NewsDTO>>
    {

    }
}
