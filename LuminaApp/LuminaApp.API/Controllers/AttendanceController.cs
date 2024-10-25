using LuminaApp.Application.Commons;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.AttendanceFeatures.Commands.AddAttendance;
using LuminaApp.Application.Features.AttendanceFeatures.Dtos;
using LuminaApp.Application.Features.AttendanceFeatures.Queries.AttendanceByStudent;
using LuminaApp.Application.Features.AttendanceFeatures.Queries.AttendancesSummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LuminaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("add-attendance")]
        public async Task<OperationResult> AddAttendance(AddAttendanceCommand attendance)
        {
            var request = await _mediator.Send(attendance);
            return request;
        }

        [HttpGet("get-attendanceById/{studentId}/{semester}")]
        public async Task<ActionResult<List<AttendanceDto>>> GetAttendances(string studentId,int semester)
        {
            try
            {
                var query = new GetAttendanceByIdQuery(studentId, semester);
                var attendances = await _mediator.Send(query);

                return Ok(attendances);
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
        [HttpGet("get-attendance-summary/{studentId}/{semester}")]
        public async Task<ActionResult<AttendanceSummeryDTO>> GetAttendancesSymmary(string studentId,int semester)
        {
            try
            {
                var query = new GetAttendancesSummaryQuery(studentId,semester);
                var attendances = await _mediator.Send(query);

                return Ok(attendances);
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
