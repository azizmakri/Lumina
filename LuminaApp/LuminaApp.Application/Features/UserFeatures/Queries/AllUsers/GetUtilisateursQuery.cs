using LuminaApp.Application.Features.UserFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.UserFeatures.Queries.AllUsers
{
    public record GetUtilisateursQuery : IRequest<List<UserDto>>;

}
