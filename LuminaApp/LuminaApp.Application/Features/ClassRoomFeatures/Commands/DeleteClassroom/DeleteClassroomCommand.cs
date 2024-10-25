using LuminaApp.Application.Commons;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.ClassRoomFeatures.Commands.DeleteClassroom
{
    public record DeleteClassroomCommand : IRequest<OperationResult> 
    {
        public DeleteClassroomCommand(int classroomId)
        {
            ClassroomId = classroomId;
        }

        public int ClassroomId { get; set; }
         
    }
}
