using LuminaApp.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SubjectFeatures.Commands.AddSubject
{
    public record AddSubjectCommand:IRequest<OperationResult>
    {
        public int gradeId { get; set; }
        public string teacherId { get; set; }
        public string SubjectName { get; set; }
        public float coefficient { get; set; }
    }
}
