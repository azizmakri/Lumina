using LuminaApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.EvaluationFeatures.Dtos
{
    public class EvaluationDTO
    {
        public float Mark { get; set; }
        public DateTime EvaluationDate { get; set; }
        public EvaluationType evaluationType { get; set; }
        public float coefficient { get; set; }
        public string SubjectName { get; set; }
        public float? ClassAverage { get; set; }
        public float? ClassHeighestMark { get; set; }
        public float? ClassLowestMark { get; set; }
    }
}
