using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.SubjectFeatures.Dtos
{
    public class subjectDTO
    {
        public int subjectId { get; set; }
        public string SubjectName { get; set; }
        public float coefficient { get; set; }

        public string TeacherName { get; set; }

    }
}
