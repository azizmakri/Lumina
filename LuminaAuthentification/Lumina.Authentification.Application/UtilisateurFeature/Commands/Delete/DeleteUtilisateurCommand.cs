using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands.Delete
{
    public class DeleteUtilisateurCommand : IRequest<Unit>
    {
        public String Id { get; set; }
    }
}
