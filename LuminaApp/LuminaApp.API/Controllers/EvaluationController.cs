using LuminaApp.Application.Commons;
using LuminaApp.Application.Commons.Exceptions;
using LuminaApp.Application.Features.EvaluationFeatures.Commands;
using LuminaApp.Application.Features.EvaluationFeatures.Dtos;
using LuminaApp.Application.Features.EvaluationFeatures.Queries.GetBulletinByStudent;
using LuminaApp.Application.Features.EvaluationFeatures.Queries.GetEvaluationByStudent;
using LuminaApp.Application.Features.EvaluationFeatures.Queries.GetEvaluationsInfosByGrade;
using LuminaApp.Application.Features.SessionFeatures.Commands.CreateSession;
using LuminaApp.Application.Features.SessionFeatures.Queries.GetByIdStudentAndDate;
using LuminaApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuminaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EvaluationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("student/{studentId}/evaluations")]
        public async Task<ActionResult<ICollection<EvaluationDTO>>> GetEvaluationsByStudent(string studentId)
        {
            try
            {
                var query = new GetEvaByStudentQuery(studentId);
                var evaluations = await _mediator.Send(query);

                return Ok(evaluations);
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

        [HttpGet("student/{studentId}/evaluation-infos")]
        public async Task<ActionResult<ICollection<EvaluationDTO>>> GetEvaluationsByGrade(string studentId)
        {
            try
            {
                var query = new GetEvaluationsInfosByGradeQuery(studentId);
                var evaluations = await _mediator.Send(query);

                return Ok(evaluations);
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


        [HttpPost]
        [Route("add-evaluation")]
        public async Task<OperationResult> AddEvaluation(CreateEvaluationCommand evaluation)
        {
            var response = await _mediator.Send(evaluation);
            return response;
        }

        [HttpGet("bulletin/{studentId}/{semester}")]
        public async Task<ActionResult<List<Evaluation>>> GetBulletinByStudent(string studentId,int semester)
        {
            var query = new GetBulletinByStudentQuery(studentId,semester);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
