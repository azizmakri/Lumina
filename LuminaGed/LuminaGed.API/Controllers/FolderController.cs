using LuminaApp.Application.Commons.Exceptions;
using LuminaGed.Application.Commons;
using LuminaGed.Application.Commons.Middlewares;
using LuminaGed.Application.Features.DocumentsFeatures.Commands.DeleteDoc;
using LuminaGed.Application.Features.FolderFeatures.Commands.CreateFolder;
using LuminaGed.Application.Features.FolderFeatures.Commands.DeleteFolder;
using LuminaGed.Application.Features.FolderFeatures.Commands.RenameFolder;
using LuminaGed.Application.Features.FolderFeatures.Queries.GetFolderByTeacher;
using LuminaGed.Application.Features.FolderFeatures.Queries.GetFoldersByStudent;
using LuminaGed.Application.Features.FolderFeatures.Queries.GetFoldersByTeacherAndNoParent;
using LuminaGed.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace LuminaGed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {

        private readonly IMediator _mediator;
        public FolderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("add-folder")]
        public async Task<OperationResult> AddFolder(CreateFolderCommand folder)
        {
            var response = await _mediator.Send(folder);
            return response;
        }

        [HttpDelete]
        [Route("{folderId}/{ownerId}")]
        public async Task<ActionResult<OperationResult>> DeleteDocument(int folderId,string ownerId)
        {
            var response = await _mediator.Send(new DeleteFolderCommand { FolderId = folderId,OwnerId=ownerId });
            return response;
        }

        [HttpGet("{folderId}")]
        public async Task<ActionResult<List<Folder>>> GetFolderById(int folderId)
        {
            try
            {
                var query = new GetFolderByIdQuery(folderId);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<List<Folder>>> GetFolderByStudent(string studentId)
        {
            try
            {
                var query = new GetFoldersByStudentQuery(studentId);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{teacherId}/{gradeId}")]
        public async Task<ActionResult<List<Folder>>> GetFolderByTeacherNoParent(string teacherId, int gradeId)
        {
            try
            {
                var query = new GetFoldersByTeacherNoParentQuery(teacherId, gradeId);
                var response = await _mediator.Send(query);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("Update-Folder")]
        public async Task<OperationResult> Update(RenameFolderCommand command)
        {
            var response = await _mediator.Send(command);
            return response;
        }

    }
}
