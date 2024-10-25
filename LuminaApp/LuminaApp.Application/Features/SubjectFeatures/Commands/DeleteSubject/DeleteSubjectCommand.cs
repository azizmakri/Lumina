using LuminaApp.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SubjectFeatures.Commands.DeleteSubject
{
    public record DeleteSubjectCommand:IRequest<OperationResult>
    {
        public DeleteSubjectCommand(int subjectId)
        {
            SubjectId = subjectId;
        }

        public int SubjectId { get; set; }
    }
}
