using LuminaApp.Application.Commons;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SessionFeatures.Commands.CreateSession
{
    public class CreateSessionCommand : IRequest<OperationResult>
    {

        public DateTime start_hour { get; set; }
        public DateTime end_hour { get; set; }
        public int subjectFK { get; set; }
        public int classRoomFK { get; set; }
    }
}
