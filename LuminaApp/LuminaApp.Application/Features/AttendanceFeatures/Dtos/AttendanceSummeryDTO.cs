
namespace LuminaApp.Application.Features.AttendanceFeatures.Dtos
{
    public record AttendanceSummeryDTO
    {
        public int totalHours { get; set; }
        public int totalAbsentHours { get; set; }
    }
}
