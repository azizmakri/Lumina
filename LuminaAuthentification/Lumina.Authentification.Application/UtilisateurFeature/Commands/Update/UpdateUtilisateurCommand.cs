using Lumina.Authentification.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.Update
{
    public class UpdateUtilisateurCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        
    }
}
