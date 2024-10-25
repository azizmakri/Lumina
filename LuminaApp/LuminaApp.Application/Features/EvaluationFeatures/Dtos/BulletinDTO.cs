using LuminaApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.EvaluationFeatures.Dtos
{
    public record BulletinDTO
    {
        public float Result { get; set; }
        public float ControleResult { get; set; }
        public float OraleResult { get; set; }
        public float SyntheseResult { get; set; }
        public float coef { get; set; }
        public string SubjectName { get; set; }
        public string Teacher { get; set; }
    }
}
