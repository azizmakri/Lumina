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

namespace LuminaApp.Application.Features.ClassRoomFeatures.Commands.CreateClassroom
{
    public class CreateClassroomCommandHandler : IRequestHandler<CreateClassroomCommand, OperationResult>
    {
        private readonly IMapper _mapper;
        private readonly IClassRoomService _classRoomService;




        public CreateClassroomCommandHandler (IMapper mapper, IClassRoomService classRoomService)
        {
            _mapper = mapper;
            _classRoomService = classRoomService;
        }

        public async Task<OperationResult> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
        {
            var classroomToCreate = _mapper.Map<ClassRoom>(request);
            try
            {
                await _classRoomService.AddClassroom(classroomToCreate);
                return new OperationResult { Status = true, Message = "salle créée avec succès" };
            }
            catch (Exception ex)
            {
                return new OperationResult { Status = false, Message = $"Échec de la création d'une salle: {ex.Message}" };
            }
        }
    }


}
