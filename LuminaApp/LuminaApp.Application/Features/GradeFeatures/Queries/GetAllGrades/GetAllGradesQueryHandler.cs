using AutoMapper;
using LuminaApp.Application.Features.GradeFeatures.Dtos;
using LuminaApp.Application.Features.SessionFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.GradeFeatures.Queries.GetAllGrades
{
    public class GetAllGradesQueryHandler : IRequestHandler<GetAllGradesQuery, ICollection<GradeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGradeService _gradeService;

        public GetAllGradesQueryHandler(IMapper mapper,IGradeService gradeService)
        {
            _mapper = mapper;
            _gradeService = gradeService;
        }
        public async Task<ICollection<GradeDto>> Handle(GetAllGradesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var grades = await _gradeService.GetAllGrades();
                var gradesDto = _mapper.Map<ICollection<GradeDto>>(grades);

                return gradesDto;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
