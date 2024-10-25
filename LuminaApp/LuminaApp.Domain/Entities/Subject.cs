using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Domain.Entities
{
    public class Subject
    {
        [Key]
        public int subjectId { get; set; }
        public int gradeFK { get; set; }
        public string teacherFK { get; set; }
        public string SubjectName { get; set; }
        public float coefficient { get; set; }
        public virtual ICollection<Session>? sessions { get; set; }
        public virtual Grade? grade { get; set; }
        public virtual User? teacher { get; set; }
    }
}
