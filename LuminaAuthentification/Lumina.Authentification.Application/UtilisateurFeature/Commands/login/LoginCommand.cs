using Lumina.Authentification.Application.Commons;
using Lumina.Authentification.Application.UtilisateurFeature.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.login
{
    public record LoginCommand : IRequest<OperationResult>
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
