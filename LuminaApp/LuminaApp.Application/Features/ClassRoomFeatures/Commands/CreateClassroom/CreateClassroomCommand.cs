using LuminaApp.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.ClassRoomFeatures.Commands.CreateClassroom
{
    public class CreateClassroomCommand:IRequest<OperationResult>
    {


        public string ClassroomName { get; set; }
    }
}
