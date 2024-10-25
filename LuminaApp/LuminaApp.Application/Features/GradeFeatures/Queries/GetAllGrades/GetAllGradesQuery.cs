using LuminaApp.Application.Features.GradeFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.GradeFeatures.Queries.GetAllGrades
{
    public record GetAllGradesQuery:IRequest<ICollection<GradeDto>>
    {
    }
}
