using Azure.Core;
using Lumina.Authentification.Application.Commons;
using Lumina.Authentification.Application.Interfaces;
using Lumina.Authentification.Application.UtilisateurFeature.Dtos;
using Lumina.Authentification.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lumina.Authentification.Infrastructure.Persistence
{
    public class UtilisateurRepository : GenericRepository<User>, IUtilisateurRepository
    {
        private readonly UserManager<User> _userManager;
        public UtilisateurRepository(UserManager<User> userManager, LuminaContext context) : base(context)
        {
            _userManager = userManager;
        }

        
          
        /* public async Task<bool> IsUtilisateurUnique(string name)
{
    return await _context.Users.AnyAsync(q=>q.FirstName==name);
}*/


    }
}
