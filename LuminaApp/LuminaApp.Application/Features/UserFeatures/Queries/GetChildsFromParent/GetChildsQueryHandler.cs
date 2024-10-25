using AutoMapper;
using LuminaApp.Application.Features.UserFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.UserFeatures.Queries.GetChildsFromParent
{
    public class GetChildsQueryHandler : IRequestHandler<GetChildsQuery, ICollection<ChildDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public GetChildsQueryHandler(IMapper mapper,IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ICollection<ChildDTO>> Handle(GetChildsQuery request, CancellationToken cancellationToken)
        {
            ICollection<User> childs = await _userService.getChildsFromParent(request.parentId);

            var childsDTO=_mapper.Map<ICollection<ChildDTO>>(childs);

            return childsDTO;
        }
    }
}
