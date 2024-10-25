using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Domain.Entities
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public virtual ICollection<Folder> Folders { get; set; }
        public virtual ICollection<User>? students { get; set; }


    }
}
