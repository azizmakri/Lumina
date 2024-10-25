using LuminaApp.Domain.Entities;

namespace LuminaApp.Application.Interfaces
{
    public interface IAttendanceService
    {
        public Task AddAttendance(Attendance attendance,string studentId,int sessionId);
        public Task<Attendance> GetStudentAttendanceBySession(string studentId, int sessionId);

        public Task<ICollection<Attendance>> GetAttendancesByStudentAndSemester(string studentId,int semester);

    }
}
