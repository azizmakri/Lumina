using LuminaGed.Application.Features.FolderFeatures.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.FolderFeatures.Queries.GetFolderByTeacher
{
    public class GetFolderByIdQuery: IRequest<FolderDto>
    {
        public int FolderId { get; set; }

        public GetFolderByIdQuery(int folderId)
        {
             FolderId= folderId;
        }
    }
}
