using LuminaApp.Application.Features.SessionFeatures.Dtos;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SessionFeatures.Queries.GetSessionById
{
    public class GetSessionByIdQuery:IRequest<SessionDTO>
    {
        public int SessionId { get; }

        public GetSessionByIdQuery(int sessionId)
        {
            SessionId = sessionId;
        }
    }
}
