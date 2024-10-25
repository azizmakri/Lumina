using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaGed.Application.Features.FolderFeatures.Dtos;
using LuminaGed.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.FolderFeatures.Queries.GetFoldersByTeacherAndNoParent
{
    public class GetFoldersByTeacherNoParentQueryHandler : IRequestHandler<GetFoldersByTeacherNoParentQuery, ICollection<FolderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IFolderService _folderService;

        public GetFoldersByTeacherNoParentQueryHandler(IMapper mapper, IFolderService folderService)
        {
            _mapper = mapper;
            _folderService = folderService;
        }
        public async Task<ICollection<FolderDto>> Handle(GetFoldersByTeacherNoParentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var folders = await _folderService.GetFolderdByTeacherAndGrade(request.TeacherId,request.GradeId);
                var folderDtos = _mapper.Map<List<FolderDto>>(folders);
                return folderDtos;
            }
            catch (ArgumentException ex)
            {
                // Handle case where student is not found
                throw new NotFoundException(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // Handle case where evaluations for the student are not available
                throw new NotFoundException(ex.Message);
            }
        }
    }
}
