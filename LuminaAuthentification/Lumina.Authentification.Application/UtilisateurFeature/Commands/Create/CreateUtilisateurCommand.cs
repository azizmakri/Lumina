using Lumina.Authentification.Application.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;
namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.Create
{
    public record CreateUtilisateurCommand : IRequest<OperationResult>
    {
        public string Role { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string? LastName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string PasswordHash { get; set; }
        public List<string>? ChildIds { get; set; } 
    }
}
