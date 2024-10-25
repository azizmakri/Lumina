using LuminaApp.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.NewsFeatures.Queries.GetNewsById
{
    public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, byte[]>
    {

        private readonly INewsService _newsService;

        public GetNewsByIdQueryHandler(INewsService docService)
        {
            _newsService = docService;
        }

        public async Task<byte[]> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _newsService.GetNewsByIdAsync(request.NewsId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Échec de la récupération du fichier actualité: {ex.Message}");
            }
        }
    }
}
