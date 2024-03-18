using Lumina.Authentification.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.ForgetPassword
{
    public record ForgetPasswordCommand:IRequest<OperationResult>
    {
        public string Email { get; set; }
    }
}
