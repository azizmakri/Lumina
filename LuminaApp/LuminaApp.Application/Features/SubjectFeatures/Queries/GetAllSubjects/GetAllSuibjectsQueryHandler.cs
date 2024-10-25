using AutoMapper;
using LuminaApp.Application.Features.SubjectFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SubjectFeatures.Queries.GetAllSubjects
{
    public class GetAllSuibjectsQueryHandler : IRequestHandler<GetAllSuibjectsQuery, List<subjectDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Subject> _subjectRepo;

        public GetAllSuibjectsQueryHandler(IMapper mapper, IGenericRepository<Subject> subjectRepo)
        {
            _mapper = mapper;
            _subjectRepo = subjectRepo;
        }
        public async Task<List<subjectDTO>> Handle(GetAllSuibjectsQuery request, CancellationToken cancellationToken)
        {
            var subjects = await _subjectRepo.GetAsync();

            var data = _mapper.Map<List<subjectDTO>>(subjects);

            return data;
        }
    }
}
