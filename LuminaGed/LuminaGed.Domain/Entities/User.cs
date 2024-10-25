using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? gradeFK { get; set; }
        public virtual Grade? grade { get; set; }
        public virtual ICollection<DocumentEntity>? teacherDocuments { get; set; }
        public virtual ICollection<DocumentEntity>? studentDocuments { get; set; }
        public virtual ICollection<Folder>? Folders { get; set; }
    }
}
