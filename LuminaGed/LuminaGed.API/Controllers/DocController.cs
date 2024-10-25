using LuminaApp.Application.Commons.Exceptions;
using LuminaGed.Application.Commons;
using LuminaGed.Application.Features.DocumentsFeatures.Commands.CreateDoc;
using LuminaGed.Application.Features.DocumentsFeatures.Commands.DeleteDoc;
using LuminaGed.Application.Features.DocumentsFeatures.Commands.GetFile;
using LuminaGed.Application.Features.DocumentsFeatures.Dtos;
using LuminaGed.Application.Features.DocumentsFeatures.Queries.AllDocuments;
using LuminaGed.Application.Features.DocumentsFeatures.Queries.GetHomeworks;
using LuminaGed.Application.Features.FolderFeatures.Commands.CreateFolder;
using LuminaGed.Application.Features.FolderFeatures.Queries.GetFoldersByTeacherAndNoParent;
using LuminaGed.Application.Interfaces;
using LuminaGed.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace LuminaGed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocController : ControllerBase
    {
        private readonly IGenericRepository<DocumentEntity> _documentRepo;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public DocController(IGenericRepository<DocumentEntity> documentRepo, IMediator mediator, IConfiguration configuration)
        {
            _documentRepo = documentRepo;
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("create-document")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<OperationResult>> CreateDocument(IFormFile file, int folderId, string userId)
        {
            if (Request.Form.Files.Count == 0)
                return NoContent();

            // Save the file to the server
            string filename = file.FileName;
            var filepath = _configuration["FileSettings:DocumentFilesPath"];
            var exactPath = Path.Combine(filepath, filename);

            using (var stream = new FileStream(exactPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create document command to be processed by the mediator
            var response = await _mediator.Send(new CreateDocCommand
            {
                DocumentName = filename,
                Creation_date = DateTime.Now,
                DocumentPath = exactPath,
                folderId = folderId,
                TeacherId = userId
            });

            return response;
        }
        [HttpDelete]
        [Route("{documentId}")]
        public async Task<ActionResult<OperationResult>> DeleteDocument(int documentId)
        {
            var response = await _mediator.Send(new DeleteDocCommand { DocumentId = documentId });
            return response;
        }
        [HttpGet]
        [HttpGet]
        [Route("get-all-documents")]
        public async Task<ActionResult<List<DocumentDto>>> GetAllDocuments()
        {
            try
            {
                var response = await _mediator.Send(new GetAllDocQuery());
                return Ok(response); // Return HTTP 200 OK with the documents
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{documentId}")]
        public async Task<IActionResult> GetFile(int documentId)
        {
            try
            {
                var fileContent = await _mediator.Send(new GetFileByIdQuery(documentId));
                var document = await _documentRepo.GetByIdAsync(documentId);
                if (document == null)
                {
                    throw new Exception($"Document with ID {documentId} not found.");
                }

                var filePath = document.DocumentPath;
                if (!System.IO.File.Exists(filePath))
                {
                    throw new FileNotFoundException("File not found on disk", filePath);
                }

                var contentType = MimeTypes.GetMimeType(filePath);
                Response.Headers.Add("Content-Disposition", $"inline; filename={Path.GetFileName(filePath)}");

                return File(fileContent, contentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("homework/{studentId}")]
        public async Task<ActionResult<ICollection<DocumentEntity>>> GetHomeworks(string studentId)
        {
            try
            {
                var query = new GetHomeworksQuery(studentId);
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

    }
}
