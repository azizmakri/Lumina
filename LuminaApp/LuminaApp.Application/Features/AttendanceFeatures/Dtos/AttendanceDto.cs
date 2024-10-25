using LuminaApp.Domain.Entities;
using LuminaApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.AttendanceFeatures.Dtos
{
    public record AttendanceDto
    {
        public string subjectName { get; set; }
        public DateTime heure_debut { get; set; }
        public DateTime heure_fin { get; set; }
        public int totalHours { get; set; }
        public int totalAbsent { get; set; }
        public AttendanceType attendanceType { get; set; }
        public string Observation { get; set; }
    }
}
