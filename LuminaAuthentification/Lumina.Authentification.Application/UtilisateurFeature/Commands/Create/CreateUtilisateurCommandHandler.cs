using AutoMapper;
using Lumina.Authentification.Application.Commons;
using Lumina.Authentification.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.CreateAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.Create
{
    public class CreateUtilisateurCommandHandler : IRequestHandler<CreateUtilisateurCommand, OperationResult>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateUtilisateurCommandHandler(IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<OperationResult> Handle(CreateUtilisateurCommand request, CancellationToken cancellationToken)
        {
            //validate incoming data
            string userRole = request.Role;
            //convert to domain entity object
            var utilisateurToCreate = _mapper.Map<User>(request);
            utilisateurToCreate.UserName = $"{request.FirstName.Trim()}{request.LastName.Trim()}";
            var userExists = await _userManager.FindByNameAsync(utilisateurToCreate.UserName);
            if (userExists != null)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Error",
                    Detail = "User already exists!"
                };
                return new OperationResult { Status = false, Message= "User already exists!" };
            }
            var result = await _userManager.CreateAsync(utilisateurToCreate, utilisateurToCreate.PasswordHash!);
            if (!result.Succeeded)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Error",
                    Detail = "User creation failed! Please check user details and try again."
                };

                var errorMessage = string.Join(", ", result.Errors.Select(error => error.Description));
                return new OperationResult { Status = false, Message = errorMessage };
            }

            if (!await _roleManager.RoleExistsAsync(userRole))
                await _roleManager.CreateAsync(new IdentityRole(userRole));
            if (await _roleManager.RoleExistsAsync(userRole))
                await _userManager.AddToRoleAsync(utilisateurToCreate, userRole);
            return new OperationResult { Status = true, Message = "User registered successfully" };
        }
    }
}
