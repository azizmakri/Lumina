using LuminaApp.Application.Commons;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.SessionFeatures.Commands.CreateSession;
using LuminaApp.Application.Features.SessionFeatures.Commands.DeleteSession;
using LuminaApp.Application.Features.SessionFeatures.Commands.UpdateSession;
using LuminaApp.Application.Features.SessionFeatures.Queries.GetAllSeesionsByTeacherId;
using LuminaApp.Application.Features.SessionFeatures.Queries.GetAllSessionsByUserId;
using LuminaApp.Application.Features.SessionFeatures.Queries.GetByIdStudentAndDate;
using LuminaApp.Application.Features.SessionFeatures.Queries.GetSessionById;
using LuminaApp.Application.Features.SessionFeatures.Queries.GetSessionsByGrade;
using LuminaApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LuminaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SessionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("add-session")]
        public async Task<OperationResult> AddSession(CreateSessionCommand session)
        {
            var response = await _mediator.Send(session);
            return response;
        }

        [HttpPut]
        [Route("")]
        public async Task<OperationResult> UpdateSession(UpdateSessionCommand session)
        {
            var response = await _mediator.Send(session);
            return response;
        }


        [HttpDelete]
        [Route("{sesionId}")]
        public async Task<ActionResult<OperationResult>> DeleteSession(int sesionId)
        {
            var response = await _mediator.Send(new DeleteSessionCommand { SessionId = sesionId });
            return response;
        }

        [HttpGet("students/{studentId}/{sessionDate}")]
        public async Task<ActionResult<List<Session>>> GetSessionsByStudentOnDate(string studentId, DateTime sessionDate)
        {
            var query = new GetSessionsByStudentOnDateQuery(studentId, sessionDate);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Session>>> GetSessionsByUserId(string userId)
        {
            var query = new GetAllSessionsByUserIdQuery(userId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("by-id/{sessionId}")]
        public async Task<ActionResult<List<Session>>> GetSessionById(int sessionId)
        {
            var query = new GetSessionByIdQuery(sessionId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult<List<Session>>> GetSessionsByTeacherId(string teacherId)
        {
            var query = new GetAllSessionsByTeachIdQuery(teacherId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("grade/{gradeId}")]
        public async Task<ActionResult<List<Session>>> GetSessionsByGrade(int gradeId)
        {
            var query = new GetSessionsByGradeQuery(gradeId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
