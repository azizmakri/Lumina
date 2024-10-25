using AutoMapper;
using LuminaGed.Application.Features.DocumentsFeatures.Dtos;
using LuminaGed.Application.Interfaces;
using LuminaGed.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class DocumentDtoRoleResolver : IValueResolver<DocumentEntity, DocumentDto, string>
{
    private readonly IGenericRepository<User> _userRepo;
    private readonly UserManager<User> _userManager;

    public DocumentDtoRoleResolver(IGenericRepository<User> userRepo, UserManager<User> userManager)
    {
        _userRepo = userRepo;
        _userManager = userManager;
    }

    public string Resolve(DocumentEntity source, DocumentDto destination, string destMember, ResolutionContext context)
    {
        var studentId = source.studentFK;
        var student = _userRepo.GetByIdAsync(studentId).Result; 
        var userRoles = _userManager.GetRolesAsync(student).Result; 

        return userRoles.FirstOrDefault(); 
    }
}
