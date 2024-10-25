using AutoMapper;
using Lumina.Authentification.Application.Commons;
using Lumina.Authentification.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Lumina.Authentification.Application.UtilisateurFeature.Commands.CreateAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lumina.Authentification.Application.Interfaces;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.Create
{
    public class CreateUtilisateurCommandHandler : IRequestHandler<CreateUtilisateurCommand, OperationResult>
    {
        private readonly IUtilisateurRepository _utilisateurRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateUtilisateurCommandHandler(IUtilisateurRepository utilisateurRepository, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _utilisateurRepository = utilisateurRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<OperationResult> Handle(CreateUtilisateurCommand request, CancellationToken cancellationToken)
        {
            // Validate incoming data
            string userRole = request.Role;

            // Convert to domain entity object
            var utilisateurToCreate = _mapper.Map<User>(request);
            utilisateurToCreate.UserName = $"{request.FirstName.Trim()}{request.LastName.Trim()}";

            // Check if user already exists
            var userExists = await _userManager.FindByNameAsync(utilisateurToCreate.UserName);
            if (userExists != null)
            {
                return new OperationResult { Status = false, Message = "L'utilisateur existe déjà !" };
            }

            // Create the user
            var result = await _userManager.CreateAsync(utilisateurToCreate, request.PasswordHash);
            if (!result.Succeeded)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(error => error.Description));
                return new OperationResult { Status = false, Message = errorMessage };
            }

            // Add role to the user
            if (!await _roleManager.RoleExistsAsync(userRole))
                await _roleManager.CreateAsync(new IdentityRole(userRole));
            if (await _roleManager.RoleExistsAsync(userRole))
                await _userManager.AddToRoleAsync(utilisateurToCreate, userRole);

            // Set child to the user if the role is "Parent"
            if (userRole == "Parent" && request.ChildIds != null && request.ChildIds.Any())
            {
                foreach (var childId in request.ChildIds)
                {
                    var childUser = await _utilisateurRepository.GetByIdAsync(childId);
                    if (childUser != null)
                    {
                        childUser.Parent = utilisateurToCreate;
                        // You might want to add this user to the child's collection as well
                        if (utilisateurToCreate.Students == null)
                            utilisateurToCreate.Students = new List<User>();
                        utilisateurToCreate.Students.Add(childUser);
                        // Update child user
                        await _utilisateurRepository.UpdateAsync(utilisateurToCreate);
                    }
                    else return new OperationResult { Status = false, Message = "L'étudiant avec l'ID " + childId + " n'a pas été trouvé" };
                }
            }

            return new OperationResult { Status = true, Message = "L'utilisateur a été enregistré avec succès" };
        }
    }
}
