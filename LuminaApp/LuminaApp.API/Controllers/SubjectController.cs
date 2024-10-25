using LuminaApp.Application.Commons;
using LuminaApp.Application.Features.SessionFeatures.Commands.CreateSession;
using LuminaApp.Application.Features.SessionFeatures.Queries.GetByIdStudentAndDate;
using LuminaApp.Application.Features.SubjectFeatures.Commands.AddSubject;
using LuminaApp.Application.Features.SubjectFeatures.Commands.DeleteSubject;
using LuminaApp.Application.Features.SubjectFeatures.Dtos;
using LuminaApp.Application.Features.SubjectFeatures.Queries.GetAllSubjects;
using LuminaApp.Application.Features.SubjectFeatures.Queries.GetSubjectsByGrade;
using LuminaApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LuminaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("get-all")]
        public async Task<List<subjectDTO>> GetAllSubjects()
        {
            var subjects = await _mediator.Send(new GetAllSuibjectsQuery());
            return subjects;
        }
        

        [HttpGet("{gradeId}")]
        public async Task<ActionResult<ICollection<Subject>>> GetSubjectsByGrade(int gradeId)
        {
            var query = new GetSubjectsByGradeQuery(gradeId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<OperationResult> AddSubject(AddSubjectCommand subject)
        {
            var response = await _mediator.Send(subject);
            return response;
        }
        [HttpDelete("{subjectId}")]
        public async Task<OperationResult> deleteSubject(int subjectId)
        {
            var command = new DeleteSubjectCommand(subjectId);

            var response = await _mediator.Send(command);
            return response;
        }
    }
}
