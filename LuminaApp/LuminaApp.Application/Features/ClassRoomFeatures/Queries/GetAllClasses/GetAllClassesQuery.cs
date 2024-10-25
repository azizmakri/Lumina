using LuminaApp.Application.Features.ClassRoomFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.ClassRoomFeatures.Queries.GetAllClasses
{
    public class GetAllClassesQuery:IRequest<IReadOnlyList<ClassRoomDTO>>
    {
    }
}
