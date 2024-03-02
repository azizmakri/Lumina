using Lumina.Authentification.Application.Interfaces;
using Lumina.Authentification.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Infrastructure.Persistence
{
    public class UtilisateurRepository : GenericRepository<User>, IUtilisateurRepository
    {
        public UtilisateurRepository(LuminaContext context) : base(context)
        {
        }
       /* public async Task<bool> IsUtilisateurUnique(string name)
        {
            return await _context.Users.AnyAsync(q=>q.FirstName==name);
        }*/
    }
}
