using LuminaApp.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.GradeFeatures.Commands.CreateGrade
{
    public class CreateGradeCommand:IRequest<OperationResult>
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public List<string> studentIds { get; set; }
    }
}
