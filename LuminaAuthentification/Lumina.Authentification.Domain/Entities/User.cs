using Microsoft.AspNetCore.Identity;

namespace Lumina.Authentification.Domain.Entities
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual User? Parent { get; set; }
        public virtual ICollection<User>? Students { get; set; }
        public string? ParentFK { get; set; }
        public int? GradeFK { get; set; }
    }
}
