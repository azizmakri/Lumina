using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.AttendanceFeatures.Dtos;
using LuminaApp.Application.Features.AttendanceFeatures.Queries;
using LuminaApp.Application.Features.EvaluationFeatures.Queries.GetBulletinByStudent;
using LuminaApp.Application.Features.UserFeatures.Dtos;
using LuminaApp.Application.Features.UserFeatures.Queries.AllUsers;
using LuminaApp.Application.Features.UserFeatures.Queries.GetChildsFromParent;
using LuminaApp.Application.Features.UserFeatures.Queries.GetSutendtsBySessionId;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuminaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{parentId}")]
        public async Task<ActionResult<ICollection<ChildDTO>>> GetChildsFromParent(string parentId)
        {
            try
            {
                var query = new GetChildsQuery(parentId);
                var childs = await _mediator.Send(query);

                return Ok(childs);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur de serveur interne: {ex.Message}");
            }
        }
        [HttpGet("get-all")]
        public async Task<List<UserDto>> GetAllUsers()
        {
            var utilisateurs = await _mediator.Send(new GetUtilisateursQuery());
            return utilisateurs;
        }


        [HttpGet("session/{sessionId}")]
        public async Task<ActionResult<ICollection<UserDto>>> GetStudentBySessionId(int sessionId)
        {
            try
            {
                var query = new GetStudentBySessionIdQuery(sessionId);
                var childs = await _mediator.Send(query);

                return Ok(childs);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur de serveur interne: {ex.Message}");
            }

        }
     }
}
