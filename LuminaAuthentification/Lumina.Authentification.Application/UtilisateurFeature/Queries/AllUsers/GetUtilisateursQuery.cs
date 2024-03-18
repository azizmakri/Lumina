using Lumina.Authentification.Application.UtilisateurFeature.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature.Queries.AllUsers
{
    public record GetUtilisateursQuery : IRequest<List<UserDto>>;
}
