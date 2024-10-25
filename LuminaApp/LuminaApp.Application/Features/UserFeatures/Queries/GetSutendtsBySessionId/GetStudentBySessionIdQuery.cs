using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuminaApp.Application.Features.UserFeatures.Dtos;
using MediatR;

namespace LuminaApp.Application.Features.UserFeatures.Queries.GetSutendtsBySessionId
{
    public class GetStudentBySessionIdQuery : IRequest<ICollection<UserDto>>
    {
        public int sessionId { get; set; }

    public GetStudentBySessionIdQuery(int sessionId)
    {
        this.sessionId = sessionId;
    }

}
}
