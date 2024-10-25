using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Domain.Entities
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public int? SubjectFK { get; set; }
        public int? ClassRoomFK { get; set; }
        public DateTime start_hour { get; set; }
        public DateTime end_hour { get; set; }
        public virtual ClassRoom? ClassRoom { get; set; }
        public virtual Subject? subject { get; set; }
        public virtual ICollection<Attendance> ? attendance { get; set; }
        public virtual ICollection<Evaluation> ? evaluations { get; set; }
    }
}
