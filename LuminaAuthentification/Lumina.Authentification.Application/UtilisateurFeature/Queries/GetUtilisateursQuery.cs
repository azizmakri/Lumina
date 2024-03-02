using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Queries
{
    public record GetUtilisateursQuery : IRequest<List<UtilisateurDto>>;
}
