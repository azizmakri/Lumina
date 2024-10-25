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

namespace LuminaApp.Application.Features.GradeFeatures.Commands.CreateGrade
{
    public class CreateGradeCommandHandler : IRequestHandler<CreateGradeCommand, OperationResult>
    {
        private readonly IMapper _mapper;
        private readonly IGradeService _gradeService;

        public CreateGradeCommandHandler(IMapper mapper,IGradeService gradeService)
        {
            _mapper = mapper;
            _gradeService = gradeService;
        }
        public async Task<OperationResult> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            var GradeToCreate = _mapper.Map<Grade>(request);
            try
            {
                await _gradeService.AddGrade(GradeToCreate, request.studentIds);
                return new OperationResult { Status = true, Message = "classe créée avec succès" };
            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la création de classe: {ex.Message}" };
            }
        }
    }
}
