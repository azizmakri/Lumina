using Lumina.Authentification.Application.UtilisateurFeature.Commands.Create;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.Update;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.Delete;
using Lumina.Authentification.Application.UtilisateurFeature.Queries.AllUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Lumina.Authentification.Application.UtilisateurFeature.Dtos;
using Lumina.Authentification.Application.Commons;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.login;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.ForgetPassword;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.ResetPassword;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.CreateAdmin;
using NPOI.XSSF.UserModel;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.CreateEleve;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lumina.Authentification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment; // Add this field

        public UtilisateurController(IMediator mediator, IWebHostEnvironment webHostEnvironment) // Modify the constructor
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment; // Assign the injected web host environment to the private field
        }

        // GET: api/<UtilisateurController>
        //[Authorize]
        [HttpGet("get-all")]
        public async Task<List<UserDto>> GetAllUsers()
        {
            var utilisateurs = await _mediator.Send(new GetUtilisateursQuery());
            return utilisateurs;
        }
        [HttpPost]
        [Route("register-admin")]
        public async Task<OperationResult> RegisterAdmin(CreateAdminCommand admin)
        {
            var response = await _mediator.Send(admin);
            return response;
        }
        [HttpPost]
        [Route("formdata")]
        public string formdatatest(IFormFile file)
        {
            return "";
        }

        // Read users from the Excel file
        [HttpPost("register-eleves-from-excel")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<List<OperationResult>>> RegisterUsersFromExcel(CancellationToken ct)
        {
            if (Request.Form.Files.Count == 0)
                return NoContent();

            var file = Request.Form.Files[0];
            var filePath = SaveFile(file);

            // Read users from the Excel file
            var usersFromExcel = ReadUsersFromExcel(filePath);

            // Register users into the database
            var results = new List<OperationResult>();
            foreach (var user in usersFromExcel)
            {
                var response = await _mediator.Send(new CreateEleveCommand
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash
                }, ct);

                results.Add(response);
            }

            return results;
        }

        // Save the uploaded file into wwwroot/uploads folder
        private string SaveFile(IFormFile file)
        {
            if (file.Length == 0)
            {
                throw new BadHttpRequestException("File is empty.");
            }

            var extension = Path.GetExtension(file.FileName);

            var webRootPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            var folderPath = Path.Combine(webRootPath, "uploads");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(folderPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            return filePath;
        }

        // Read users from the Excel file
        private List<ExcelUser> ReadUsersFromExcel(string filePath)
        {
            var users = new List<ExcelUser>();

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var workbook = new XSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(0); // Assuming the first sheet contains user data

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) // Assuming first row is header
                {
                    var row = sheet.GetRow(i);
                    if (row == null)
                        continue;

                    users.Add(new ExcelUser
                    {
                        FirstName = row.GetCell(0)?.ToString(),
                        LastName = row.GetCell(1)?.ToString(),
                        Email = row.GetCell(2)?.ToString(),
                        PasswordHash = row.GetCell(3)?.ToString()
                    });
                }
            }

            return users;
        }
        // POST api/<UtilisateurController>
        [HttpPost("register")]
        public async Task<OperationResult> RegisterUser([FromBody] CreateUtilisateurCommand utilisateur)
        {
            var response = await _mediator.Send(utilisateur);
            return response;
        }
        [HttpPost("login")]
        public async Task<OperationResult> LoginUser(LoginCommand utilisateur)
        {
            var response = await _mediator.Send(utilisateur);
            return response;
        }
        [HttpPost("forget-password")]
        public async Task<OperationResult> ForgetPassword(ForgetPasswordCommand utilisateur)
        {
            var response = await _mediator.Send(utilisateur);
            return response;
        }
        [HttpPost("reset-password")]
        public async Task<OperationResult> ResetPassword(ResetPasswordCommand utilisateur)
        {
            var response = await _mediator.Send(utilisateur);
            return response;
        }

        // PUT api/<UtilisateurController>/5
        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateUser(UpdateUtilisateurCommand utilisateur)
        {
            await _mediator.Send(utilisateur);
            return NoContent();
        }

        // DELETE api/<UtilisateurController>/5
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var Command = new DeleteUtilisateurCommand { Id = id };
            await _mediator.Send(Command);
            return NoContent();
        }
    }
}
