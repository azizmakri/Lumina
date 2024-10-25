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
                    Title = "Erreur",
                    Detail = "L'utilisateur existe déjà !"
                };
                return new OperationResult { Status = false, Message= "L'utilisateur existe déjà !" };
            }
            var result = await _userManager.CreateAsync(utilisateurToCreate, utilisateurToCreate.PasswordHash!);
            if (!result.Succeeded)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Erreur",
                    Detail = "La création d'un utilisateur a échoué ! Veuillez vérifier les détails de l'utilisateur et réessayer."
                };

                var errorMessage = string.Join(", ", result.Errors.Select(error => error.Description));
                return new OperationResult { Status = false, Message = errorMessage };
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Student))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Student));
            if (await _roleManager.RoleExistsAsync(UserRoles.Student))
                await _userManager.AddToRoleAsync(utilisateurToCreate, UserRoles.Student);

            return new OperationResult { Status = true, Message = "L'utilisateur a été enregistré avec succès" };
        }
    }
}
