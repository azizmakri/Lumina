using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaGed.Application.Features.FolderFeatures.Dtos;
using LuminaGed.Application.Interfaces;
using LuminaGed.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LuminaGed.Application.Features.FolderFeatures.Queries.GetFolderByTeacher
{
    public class GetFolderByIdQueryHandler : IRequestHandler<GetFolderByIdQuery, FolderDto>
    {
        private readonly IMapper _mapper;
        private readonly IFolderService _folderService;
        private readonly IGenericRepository<User> _userRepo;
        private readonly UserManager<User> _userManager;


        public GetFolderByIdQueryHandler(IGenericRepository<User> userRepo, UserManager<User> userManager, IMapper mapper, IFolderService folderService)
        {
            _mapper = mapper;
            _folderService = folderService;
            _userManager = userManager;
            _userRepo = userRepo;

        }

        public async Task<FolderDto> Handle(GetFolderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var folder = await _folderService.GetFolderById(request.FolderId);
                var folderDto = _mapper.Map<FolderDto>(folder);

                // Fetch and map the student roles for each document
                if (folderDto.Documents != null)
                {
                    foreach (var document in folderDto.Documents)
                    {
                        if (!string.IsNullOrEmpty(document.studentId))
                        {
                            var student = await _userRepo.GetByIdAsync(document.studentId);
                            var userRoles = await _userManager.GetRolesAsync(student);
                            document.StudentRole = userRoles.FirstOrDefault(); // Assuming single role per user
                        }
                        else
                        {
                            document.StudentRole = null;
                        }
                    }
                }

                return folderDto;
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

