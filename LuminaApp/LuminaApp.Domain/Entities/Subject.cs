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
        public string SubjectName { get; set; }
        public float coefficient { get; set; }
        public virtual ICollection<Session>? sessions { get; set; }
    }
}
