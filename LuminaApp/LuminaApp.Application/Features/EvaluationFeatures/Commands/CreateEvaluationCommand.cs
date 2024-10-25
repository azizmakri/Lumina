using LuminaApp.Application.Commons;
using LuminaApp.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.EvaluationFeatures.Commands
{
    public class CreateEvaluationCommand : IRequest<OperationResult>
    {
        public float Mark { get; set; }
        public EvaluationType EvaluationType { get; set; }
        public int sessionId { get; set; }
        public string StudentId { get; set; }
    }
}
