using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaGed.Application.Features.FolderFeatures.Dtos;
using LuminaGed.Application.Interfaces;
using MediatR;

namespace LuminaGed.Application.Features.FolderFeatures.Queries.GetFoldersByStudent
{
    public class GetFoldersByStudentQueryHandler : IRequestHandler<GetFoldersByStudentQuery, ICollection<FolderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IFolderService _folderService;

        public GetFoldersByStudentQueryHandler(IMapper mapper,IFolderService folderService)
        {
            _mapper = mapper;
            _folderService = folderService;
        }

        public async Task<ICollection<FolderDto>> Handle(GetFoldersByStudentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var folders = await _folderService.GetFoldersByStudent(request.StudentId);
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
