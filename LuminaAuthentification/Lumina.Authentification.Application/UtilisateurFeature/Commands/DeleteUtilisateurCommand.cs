using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands
{
    public class DeleteUtilisateurCommand : IRequest<Unit>
    {
        public int Identifiant { get; set; } 
    }
}
