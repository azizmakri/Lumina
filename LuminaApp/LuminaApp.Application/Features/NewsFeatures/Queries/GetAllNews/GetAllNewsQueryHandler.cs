using AutoMapper;
using LuminaApp.Application.Features.NewsFeatures.Dtos;
using LuminaApp.Application.Features.SubjectFeatures.Dtos;
using LuminaApp.Application.Features.SubjectFeatures.Queries.GetAllSubjects;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.NewsFeatures.Queries.GetAllNews
{
    public class GetAllNewsQueryHandler : IRequestHandler<GetAllNewsQuery, List<NewsDTO>>
    {


        private readonly IMapper _mapper;
        private readonly IGenericRepository<SchoolNews> _newsRepo;


       public GetAllNewsQueryHandler(IMapper mapper, IGenericRepository<SchoolNews> newsRepo)
        {
            _mapper = mapper;
            _newsRepo = newsRepo;
        }

        public async Task<List<NewsDTO>> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
        {
            var news = await _newsRepo.GetAsync();

            var data = _mapper.Map<List<NewsDTO>>(news);

            return data;
        }
    }
}
