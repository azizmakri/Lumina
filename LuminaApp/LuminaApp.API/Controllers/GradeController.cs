using LuminaApp.Application.Commons;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.GradeFeatures.Commands.CreateGrade;
using LuminaApp.Application.Features.GradeFeatures.Dtos;
using LuminaApp.Application.Features.GradeFeatures.Queries.GetAllGrades;
using LuminaApp.Application.Features.GradeFeatures.Queries.GetGradesByTeacher;
using LuminaApp.Application.Features.UserFeatures.Dtos;
using LuminaApp.Application.Features.UserFeatures.Queries.AllUsers;
using LuminaApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LuminaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GradeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<OperationResult> AddAttendance(CreateGradeCommand grade)
        {
            var request = await _mediator.Send(grade);
            return request;
        }

        [HttpGet]
        public async Task<ICollection<GradeDto>> GetAllGrades()
        {
            var grades = await _mediator.Send(new GetAllGradesQuery());
            return grades;
        }

        [HttpGet("{teacherId}")]
        public async Task<ActionResult<List<Grade>>> GetFolderByTeacherNoParentQuery(string teacherId)
        {
            try
            {
                var query = new GetGradesByTeacherQuery(teacherId);
                var response = await _mediator.Send(query);
                return Ok(response);
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
