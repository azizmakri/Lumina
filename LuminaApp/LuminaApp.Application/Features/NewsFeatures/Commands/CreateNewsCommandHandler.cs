using AutoMapper;
using LuminaApp.Application.Commons;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.NewsFeatures.Commands
{
    public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, OperationResult>
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public CreateNewsCommandHandler(IMapper mapper, INewsService newsService)
        {
            _mapper = mapper;
            _newsService = newsService;
        }

        public async Task<OperationResult> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
        {
            var newsToCreate = _mapper.Map<SchoolNews>(request);

            try
            {
                await _newsService.AddNews(newsToCreate, request.AdminId);
                return new OperationResult { Status = true, Message = "Actualités créées avec succès" };
            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la création des Actualités: {ex.Message}" };
            }
        }

    }
}