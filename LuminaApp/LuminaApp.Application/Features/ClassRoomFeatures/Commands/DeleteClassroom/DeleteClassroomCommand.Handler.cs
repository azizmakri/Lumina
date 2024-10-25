using AutoMapper;
using LuminaApp.Application.Commons;
using LuminaApp.Application.Features.SubjectFeatures.Commands.DeleteSubject;
using LuminaApp.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.ClassRoomFeatures.Commands.DeleteClassroom
{
    public class DeleteClassroomCommandHandler : IRequestHandler<DeleteClassroomCommand, OperationResult>
    {


        private readonly IMapper _mapper;
        private readonly IClassRoomService _classroomService;
       

        public DeleteClassroomCommandHandler(IMapper mapper, IClassRoomService classroomService)
        {
            _mapper = mapper;
            _classroomService = classroomService;
        }

        public async Task<OperationResult> Handle(DeleteClassroomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _classroomService.DeleteClassroom(request.ClassroomId);
                return new OperationResult { Status = true, Message = $"Salle avec l'id {request.ClassroomId} supprimée avec succès" };
            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la suppression d'une salle avec l'Id {request.ClassroomId}: {ex.Message}" };
            }
        }
    }
}
