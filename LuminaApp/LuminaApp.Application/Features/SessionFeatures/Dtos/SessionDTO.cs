using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SessionFeatures.Dtos
{
    public class SessionDTO
    {
        public int SessionId { get; set; }
        public DateTime start_hour { get; set; }
        public DateTime end_hour { get; set; }
        public string classroomName { get; set; }
        public string subjectName { get; set; }
        public int gradeId { get; set; }
        public string gradeName { get; set; }

    }
}
