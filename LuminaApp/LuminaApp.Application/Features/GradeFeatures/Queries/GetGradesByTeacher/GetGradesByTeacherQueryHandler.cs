using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.GradeFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.GradeFeatures.Queries.GetGradesByTeacher
{
    public class GetGradesByTeacherQueryHandler : IRequestHandler<GetGradesByTeacherQuery, ICollection<GradeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGradeService _gradeService;

        public GetGradesByTeacherQueryHandler(IMapper mapper,IGradeService gradeService)
        {
            _mapper = mapper;
            _gradeService = gradeService;
        }
        public async Task<ICollection<GradeDto>> Handle(GetGradesByTeacherQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var grades = await _gradeService.GetGradesByTeacher(request.TeacherId);
                var gradeDtos = _mapper.Map<List<GradeDto>>(grades);
                return gradeDtos;
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
