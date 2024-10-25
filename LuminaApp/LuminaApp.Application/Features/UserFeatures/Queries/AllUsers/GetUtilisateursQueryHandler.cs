using AutoMapper;
using LuminaApp.Application.Features.UserFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LuminaApp.Application.Features.UserFeatures.Queries.AllUsers
{
    public class GetUtilisateursQueryHandler : IRequestHandler<GetUtilisateursQuery, List<UserDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _utilisateurRepository;
        private readonly UserManager<User> _userManager;

        public GetUtilisateursQueryHandler(IMapper mapper, IGenericRepository<User> utilisateurRepository, UserManager<User> userManager)
        {
            _mapper = mapper;
            _utilisateurRepository = utilisateurRepository;
            _userManager = userManager;
        }

        public async Task<List<UserDto>> Handle(GetUtilisateursQuery request, CancellationToken cancellationToken)
        {
            // Query the Database
            var utilisateurs = await _utilisateurRepository.GetAsync();

            // Convert data objects into DTO objects
            var data = _mapper.Map<List<UserDto>>(utilisateurs);

            // Retrieve and set role for each user
            foreach (var userDto in data)
            {
                var user = _userManager.Users.FirstOrDefault(u => u.Email == userDto.Email);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    userDto.role = roles.Count > 0 ? roles[0] : "pas de role"; // Set the first role or handle multiple roles
                }
            }

            // Return the list of DTO objects
            return data;
        }
    }
}
