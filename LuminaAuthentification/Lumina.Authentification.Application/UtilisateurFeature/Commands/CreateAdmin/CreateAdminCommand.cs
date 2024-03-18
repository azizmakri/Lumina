using Lumina.Authentification.Application.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.CreateAdmin
{
    public record CreateAdminCommand:IRequest<OperationResult>
    {
        [Required(ErrorMessage = "User First Name is required")]
        public string? FirstName { get; set; } 
        [Required(ErrorMessage = "User Last Name is required")]
        public string? LastName { get; set; } 
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; } 

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; }
    }
}
