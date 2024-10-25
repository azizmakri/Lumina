using LuminaApp.Application.Commons;
using LuminaApp.Application.Features.ClassRoomFeatures.Commands.CreateClassroom;
using LuminaApp.Application.Features.ClassRoomFeatures.Commands.DeleteClassroom;
using LuminaApp.Application.Features.ClassRoomFeatures.Dtos;
using LuminaApp.Application.Features.ClassRoomFeatures.Queries.GetAllClasses;
using LuminaApp.Application.Features.SubjectFeatures.Commands.AddSubject;
using LuminaApp.Application.Features.SubjectFeatures.Commands.DeleteSubject;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LuminaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRoomController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClassRoomController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IReadOnlyCollection<ClassRoomDTO>> GetAllClasses()
        {
            var classes = await _mediator.Send(new GetAllClassesQuery());
            return classes;
        }


        [HttpPost]
        public async Task<OperationResult> AddClassroom(CreateClassroomCommand classRoom)
        {
            var response = await _mediator.Send(classRoom);
            return response;
        }

        [HttpDelete("{ClassroomId}")]
        public async Task<OperationResult> DeleteClassroom(int ClassroomId)
        {
            var command = new DeleteClassroomCommand(ClassroomId);

            var response = await _mediator.Send(command);
            return response;
        }

    } 
}
