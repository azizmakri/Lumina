using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuminaApp.Application.Features.SessionFeatures.Dtos;
using MediatR;

namespace LuminaApp.Application.Features.SessionFeatures.Queries.GetAllSessionsByUserId
{
    public class GetAllSessionsByUserIdQuery : IRequest<List<SessionDTO>>
    {
        public string StudentId { get; }

        public GetAllSessionsByUserIdQuery(string studentId)
        {
            StudentId = studentId;
        }

    }
}
