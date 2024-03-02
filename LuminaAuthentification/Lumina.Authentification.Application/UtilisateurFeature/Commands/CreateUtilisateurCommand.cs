using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Commands
{
    public class CreateUtilisateurCommand:IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
