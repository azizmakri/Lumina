using AutoMapper;
using LuminaApp.Application.Interfaces;
using MediatR;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.EvaluationFeatures.Dtos;

namespace LuminaApp.Application.Features.EvaluationFeatures.Queries.GetEvaluationByStudent
{
    public class GetEvaByStudentQueryHandler : IRequestHandler<GetEvaByStudentQuery, ICollection<EvaluationDTO>>
    {
        private readonly IEvaluationService _evaluationService;
        private readonly IMapper _mapper;

        public GetEvaByStudentQueryHandler(IEvaluationService evaluationService, IMapper mapper)
        {
            _evaluationService = evaluationService;
            _mapper = mapper;
        }

        public async Task<ICollection<EvaluationDTO>> Handle(GetEvaByStudentQuery request, CancellationToken cancellationToken)
        {
                // Retrieve evaluations for the specified student
                var evaluations = await _evaluationService.GetEvaluationsByStudent(request.StudentId);

                // Map the evaluations to DTOs
                var evaluationDTOs = _mapper.Map<ICollection<EvaluationDTO>>(evaluations);

                return evaluationDTOs;
        }
    }
}
