using LuminaApp.Domain.Entities;

namespace LuminaApp.Application.Interfaces
{
    public interface IGradeService
    {
        public Task AddGrade(Grade grade, List<string> studentIds);
        public Task<ICollection<Grade>> GetAllGrades();
        public Task<ICollection<Grade>> GetGradesByTeacher(string TeacherId);


    }
}
