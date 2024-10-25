using LuminaApp.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SessionFeatures.Commands.DeleteSession
{
    public class DeleteSessionCommand : IRequest<OperationResult>
    {
        public int SessionId { get; set; }
    }
}
