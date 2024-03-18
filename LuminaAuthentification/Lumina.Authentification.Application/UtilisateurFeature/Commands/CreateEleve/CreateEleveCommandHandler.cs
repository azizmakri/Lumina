using AutoMapper;
using Lumina.Authentification.Application.Commons;
using Lumina.Authentification.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.CreateEleve
{
    public class CreateEleveCommandHandler : IRequestHandler<CreateEleveCommand, OperationResult>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateEleveCommandHandler(IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<OperationResult> Handle(CreateEleveCommand request, CancellationToken cancellationToken)
        {
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

            if (!await _roleManager.RoleExistsAsync(UserRoles.Eleve))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Eleve));
            if (await _roleManager.RoleExistsAsync(UserRoles.Eleve))
                await _userManager.AddToRoleAsync(utilisateurToCreate, UserRoles.Eleve);

            return new OperationResult { Status = true, Message = "User registered successfully" };
        }
    }
}
