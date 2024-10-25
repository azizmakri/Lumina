using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Domain.Entities
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Subject>? subjects { get; set; }
        public virtual ICollection<User>? students { get; set; }

        public virtual ICollection<Folder>? Folders { get; set; }
    }
}
