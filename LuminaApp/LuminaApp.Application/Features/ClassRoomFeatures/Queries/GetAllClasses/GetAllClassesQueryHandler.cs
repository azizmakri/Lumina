using AutoMapper;
using LuminaApp.Application.Features.ClassRoomFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.ClassRoomFeatures.Queries.GetAllClasses
{
    public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesQuery, IReadOnlyList<ClassRoomDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IClassRoomService _classRoomService;

        public GetAllClassesQueryHandler(IMapper mapper,IClassRoomService classRoomService)
        {
            _mapper = mapper;
            _classRoomService = classRoomService;
        }
        public async Task<IReadOnlyList<ClassRoomDTO>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {
            var classes=await _classRoomService.GetAllClasses();
            var classDTO=_mapper.Map<IReadOnlyList<ClassRoomDTO>>(classes);
            return classDTO;
        }
    }
}
