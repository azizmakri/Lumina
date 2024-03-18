using Lumina.Authentification.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.ResetPassword
{
    public record ResetPasswordCommand:IRequest<OperationResult>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
