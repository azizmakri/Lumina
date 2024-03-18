using Lumina.Authentification.Domain.Entities;
namespace Lumina.Authentification.Application.Interfaces
{
    public interface IUtilisateurRepository : IGenericRepository<User>
    {
        //Task<bool> IsUtilisateurUnique(string name);
        //List<User> ReadUsersFromExcel(Stream stream);
    }
}
