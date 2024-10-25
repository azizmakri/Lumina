using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuminaApp.Application.Features.SessionFeatures.Dtos;
using MediatR;

namespace LuminaApp.Application.Features.SessionFeatures.Queries.GetAllSeesionsByTeacherId
{
    public class GetAllSessionsByTeachIdQuery : IRequest<List<SessionDTO>>
    {
        public string TeacherId { get; }

        public GetAllSessionsByTeachIdQuery(string teacherId)
        {
            TeacherId = teacherId;
        }

    }
  
}
