using AutoMapper;
using LuminaApp.Application.Commons;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;

namespace LuminaApp.Application.Features.SubjectFeatures.Commands.AddSubject
{
    public class AddSubjectCommandHandler : IRequestHandler<AddSubjectCommand, OperationResult>
    {
        private readonly IMapper _mapper;
        private readonly ISubjectService _subjectService;

        public AddSubjectCommandHandler(IMapper mapper,ISubjectService subjectService)
        {
            _mapper = mapper;
            _subjectService = subjectService;
        }
        public async Task<OperationResult> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            var subjectToCreate = _mapper.Map<Subject>(request);
            try
            {
                await _subjectService.AddSubject(subjectToCreate, request.teacherId, request.gradeId);
                return new OperationResult { Status = true, Message = "Matière créée avec succès" };
            }catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la création d'une matière: {ex.Message}" };
            }
        }
    }
}
