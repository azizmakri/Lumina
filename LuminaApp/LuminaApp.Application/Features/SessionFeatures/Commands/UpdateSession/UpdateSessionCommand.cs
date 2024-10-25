using LuminaApp.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SessionFeatures.Commands.UpdateSession
{
    public class UpdateSessionCommand:IRequest<OperationResult>
    {
        public int sessionId { get; set; }
        public DateTime start_hour { get; set; }
        public DateTime end_hour { get; set; }
    }
}
