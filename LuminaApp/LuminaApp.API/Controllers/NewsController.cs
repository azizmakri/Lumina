using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LuminaApp.Application.Features.NewsFeatures.Commands;
using LuminaApp.Application.Commons;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using LuminaApp.Application.Features.NewsFeatures.Queries.GetAllNews;
using LuminaApp.Application.Features.NewsFeatures.Dtos;
using LuminaApp.Application.Features.NewsFeatures.Queries.GetNewsById;
using MimeKit;
using static NuGet.Packaging.PackagingConstants;
using LuminaApp.Application.Features.ClassRoomFeatures.Dtos;
using LuminaApp.Application.Features.ClassRoomFeatures.Queries.GetAllClasses;
using LuminaApp.Application.Features.NotificationFeatures.Dtos;
using LuminaApp.Application.Features.NotificationFeatures.Queries;
using LuminaApp.Application.Features.NotificationFeatures.Commands;


namespace LuminaApp.Api.Controllers
{

    
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IGenericRepository<SchoolNews> _newsRepo;

        public NewsController(IConfiguration configuration,IMediator mediator, IGenericRepository<SchoolNews> newsRepo)
        {
            _configuration = configuration;
            _mediator = mediator;
            _newsRepo = newsRepo;
        }

        [HttpPost]
        [Route("create-news")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<OperationResult>> CreateNews(IFormFile file, string adminId)
        {
            if (Request.Form.Files.Count == 0)
                return NoContent();

            // Save the file to the server
            string filename = file.FileName;
            var filepath = _configuration["NewsFileSettings:NewsFilesPath"];
            var exactPath = Path.Combine(filepath, filename);

            using (var stream = new FileStream(exactPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create news command to be processed by the mediator
            var response = await _mediator.Send(new CreateNewsCommand
            {
                Title = filename,
                NewsDate = DateTime.Now,
                AdminId = adminId,
                NewsPath = exactPath,
            });

            return response;
        }
        [HttpPut]
        [Route("read")]
        public async Task<OperationResult> ReadNotif(UpdateNotificationCommand notification)
        {
            var response = await _mediator.Send(notification);
            return response; 
        }

        [HttpGet]
        [Route("get-all-News")]
        public async Task<ActionResult<List<NewsDTO>>> GetAllNews()
        {
            try
            {
                var response = await _mediator.Send(new GetAllNewsQuery());
                return Ok(response); // Return HTTP 200 OK with the news
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, $"Erreur de serveur interne: {ex.Message}");
            }
        }

        [HttpGet("{NewsId}")]
        public async Task<IActionResult> GetNewsFile(int NewsId)
        {
            try
            {
                var fileContent = await _mediator.Send(new GetNewsByIdQuery(NewsId));
                var document = await _newsRepo.GetByIdAsync(NewsId);
                if (document == null)
                {
                    throw new Exception($"Le document avec l'ID {NewsId} n'a pas été trouvé.");
                }

                var filePath = document.NewsPath;
                if (!System.IO.File.Exists(filePath))
                {
                    throw new FileNotFoundException("Fichier non trouvé sur le disque", filePath);
                }

                var contentType = MimeTypes.GetMimeType(filePath);
                var encodedFileName = Uri.EscapeDataString(Path.GetFileName(filePath));
                Response.Headers.Add("Content-Disposition", $"inline; filename*=UTF-8''{encodedFileName}");

                return File(fileContent, contentType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur de serveur interne: {ex.Message}");
            }
        }


        [HttpGet("notifications")]
        public async Task<IReadOnlyCollection<NotificationDTO>> GetAllNotifications()
        {
            var classes = await _mediator.Send(new GetAllNotificationsQuery());
            return classes;
        }
    }
}





