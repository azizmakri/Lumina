using LuminaApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Domain.Entities
{
    public class Evaluation
    {
        [Key]
        public int EvaluationId { get; set; }
        public float Mark { get; set; }
        public DateTime EvaluationDate { get; set; }
        public EvaluationType evaluationType { get; set; }
        public float coefficient { get; set; }
        public string SubjectName { get; set; }
        public virtual User? Teacher { get; set; }
        public string? TeacherFk { get; set; }
        public string StudentFk { get; set; }
        public virtual  User? Student { get; set; }
    }
}
