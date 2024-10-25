using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.SessionFeatures.Dtos;
using LuminaApp.Application.Features.SubjectFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SubjectFeatures.Queries.GetSubjectsByGrade
{
    public class GetSubjectsByGradeQueryHandler : IRequestHandler<GetSubjectsByGradeQuery, ICollection<subjectDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ISubjectService _subjectService;

        public GetSubjectsByGradeQueryHandler(IMapper mapper,ISubjectService subjectService)
        {
            _mapper = mapper;
            _subjectService = subjectService;
        }
        public async Task<ICollection<subjectDTO>> Handle(GetSubjectsByGradeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var subjects = await _subjectService.GetSubjectsByGrade(request.GradeId);
                var subjectsDto = _mapper.Map<ICollection<subjectDTO>>(subjects);

                return subjectsDto;
            }
            catch (ArgumentException ex)
            {
                // Handle case where student is not found
                throw new NotFoundException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Handle case where evaluations for the student are not available
                throw new NotFoundException(ex.Message);
            }
        }
    }
}
