using LuminaApp.Application.Features.UserFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.UserFeatures.Queries.GetChildsFromParent
{
    public class GetChildsQuery: IRequest<ICollection<ChildDTO>>
    {
        public string parentId { get; set; }

        public GetChildsQuery(string parentId)
        {
            this.parentId = parentId;
        }

    }
}
