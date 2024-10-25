using AutoMapper;
using LuminaApp.Application.Commons;
using LuminaApp.Application.Features.SessionFeatures.Commands.CreateSession;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.EvaluationFeatures.Commands
{
    public class CreateEvaluationCommandHandler : IRequestHandler<CreateEvaluationCommand, OperationResult>
    {
        private readonly IEvaluationService _evaluationService;
        private readonly IMapper _mapper;

        public CreateEvaluationCommandHandler(IEvaluationService evaluationService,IMapper mapper)
        {
            _evaluationService = evaluationService;
            _mapper = mapper;
        }

        public async Task<OperationResult> Handle(CreateEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluationToCreate = _mapper.Map<Evaluation>(request);
            try
            {
                // Create the evaluation using the provided information
                await _evaluationService.CreateEvaluation(evaluationToCreate, request.sessionId, request.StudentId);

                return new OperationResult { Status = true, Message = "L'évaluation a été créée avec succès" };
            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la création d'une évaluation: {ex.Message}" };
            }
        }
    }
}
