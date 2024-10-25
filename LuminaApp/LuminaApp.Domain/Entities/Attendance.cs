using LuminaApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace LuminaApp.Domain.Entities
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        public string? StudentFK { get; set; }
        public int? SessionFK { get; set; }
        public AttendanceType attendanceType { get; set; }
        public string ? Observation { get; set; }

        public virtual Session? session { get; set; }
        public virtual User? Student { get; set; }
    }
}
