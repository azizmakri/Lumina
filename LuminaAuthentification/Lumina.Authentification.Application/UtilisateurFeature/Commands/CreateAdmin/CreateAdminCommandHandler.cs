using AutoMapper;
using Lumina.Authentification.Application.Commons;
using Lumina.Authentification.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.CreateAdmin
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, OperationResult>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateAdminCommandHandler(IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<OperationResult> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            //validate incoming data

            //convert to domain entity object
            var utilisateurToCreate = _mapper.Map<User>(request);
            utilisateurToCreate.UserName = $"{request.FirstName.Trim()}{request.LastName.Trim()}";
            var userExists = await _userManager.FindByNameAsync(utilisateurToCreate.UserName);
            if (userExists != null)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Erreur",
                    Detail = "L'utilisateur existe déjà !"
                };
                return new OperationResult { Status = false, Message= "L'utilisateur existe déjà !" };
            }
            var result = await _userManager.CreateAsync(utilisateurToCreate, utilisateurToCreate.PasswordHash!);
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(utilisateurToCreate, UserRoles.Admin);

            if (!result.Succeeded)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Erreur",
                    Detail = "La création de l'utilisateur a échoué ! Veuillez vérifier les détails de l'utilisateur et réessayer."
                };

                var errorMessage = string.Join(", ", result.Errors.Select(error => error.Description));
                return new OperationResult { Status = false, Message = errorMessage };
            }

            
            return new OperationResult { Status = true, Message = "L'utilisateur a été enregistré avec succès" };
        }
    }

}
