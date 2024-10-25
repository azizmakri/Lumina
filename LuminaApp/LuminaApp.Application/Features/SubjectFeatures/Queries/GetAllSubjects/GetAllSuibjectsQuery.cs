using LuminaApp.Application.Features.SubjectFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SubjectFeatures.Queries.GetAllSubjects
{
    public class GetAllSuibjectsQuery : IRequest<List<subjectDTO>>
    {
    }
}
