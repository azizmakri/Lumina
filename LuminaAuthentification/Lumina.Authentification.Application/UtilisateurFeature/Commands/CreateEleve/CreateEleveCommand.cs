using Lumina.Authentification.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.CreateEleve
{
    public class CreateEleveCommand : IRequest<OperationResult>
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
