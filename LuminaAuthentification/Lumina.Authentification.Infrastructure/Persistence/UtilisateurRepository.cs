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

        //public List<User> ReadUsersFromExcel(Stream stream)
        //{
        //    var users = new List<User>();

        //    using (var package = new ExcelPackage(stream))
        //    {
        //        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
        //        if (worksheet != null)
        //        {
        //            int rowCount = worksheet.Dimension.Rows;

        //            for (int row = 2; row <= rowCount; row++) // Assuming first row is header
        //            {
        //                users.Add(new UserDto
        //                {
        //                    FirstName = worksheet.Cells[row, 1].Value?.ToString(),
        //                    LastName = worksheet.Cells[row, 2].Value?.ToString(),
        //                    Email = worksheet.Cells[row, 3].Value?.ToString(),
        //                    PasswordHash = worksheet.Cells[row, 4].Value?.ToString()
        //                });
        //            }
        //        }
        //    }

        //    return users;
        //}
          
        /* public async Task<bool> IsUtilisateurUnique(string name)
{
    return await _context.Users.AnyAsync(q=>q.FirstName==name);
}*/


    }
}
