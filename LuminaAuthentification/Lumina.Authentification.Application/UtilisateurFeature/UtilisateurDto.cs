using Lumina.Authentification.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumina.Authentification.Application.UtilisateurFeature
{
    public class UtilisateurDto
    {
         public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
