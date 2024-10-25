using AutoMapper;
using LuminaApp.Application.Commons;
using LuminaApp.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SubjectFeatures.Commands.DeleteSubject
{
    public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, OperationResult>
    {
        private readonly IMapper _mapper;
        private readonly ISubjectService _subjectService;

        public DeleteSubjectCommandHandler(IMapper mapper,ISubjectService subjectService)
        {
            _mapper = mapper;
            _subjectService = subjectService;
        }
        public async Task<OperationResult> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _subjectService.DeleteSubject(request.SubjectId);
                return new OperationResult { Status = true, Message = $"Matière avec l'id {request.SubjectId} supprimée avec succès" };
            }
            catch (Exception ex) 
            {
                return new OperationResult { Status = false, Message = $"Échec de la suppression d'une matière avec l'Id {request.SubjectId}: {ex.Message}" };
            }
        }
    }
}
