using Lumina.Authentification.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.Interfaces
{
    public interface IUtilisateurRepository :IGenericRepository<User>
    {
        //Task<bool> IsUtilisateurUnique(string name);
    }
}
