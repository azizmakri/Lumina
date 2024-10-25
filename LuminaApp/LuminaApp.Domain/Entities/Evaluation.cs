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
        public float? Mark { get; set; }
        public int? rating { get; set; }
        public string? description { get; set; }
        public EvaluationType evaluationType { get; set; }
        public virtual Session? session { get; set; }
        public virtual User? student { get; set; }
        public int? sessionFk { get; set; }
        public string? studentFk { get; set; }
    }
}
