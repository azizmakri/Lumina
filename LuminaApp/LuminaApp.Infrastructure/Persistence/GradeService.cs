
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;

namespace LuminaApp.Infrastructure.Persistence
{
    public class GradeService : IGradeService
    {
        private readonly IGenericRepository<Grade> _gradeRepo;
        private readonly IGenericRepository<User> _userRepo;

        public GradeService(IGenericRepository<Grade> gradeRepo,IGenericRepository<User> userRepo)
        {
            _gradeRepo = gradeRepo;
            _userRepo = userRepo;
        }

        public async Task AddGrade(Grade grade, List<string> studentIds)
        {
            ICollection<User> students=new List<User>();

                foreach (var studentId in studentIds)
                {
                    User student = await _userRepo.GetByIdAsync(studentId);
                    if (student != null && student.grade== null)
                    {
                        students.Add(student);
                    }
                }
                grade.students = students;
                await _gradeRepo.CreateAsync(grade);
        }

        public async Task<ICollection<Grade>> GetAllGrades()
        {
            var grades = await _gradeRepo.GetAsync();
            return (ICollection<Grade>)grades;
        }

        public async Task<ICollection<Grade>> GetGradesByTeacher(string TeacherId)
        {
            User teacher = await _userRepo.GetByIdAsync(TeacherId);
            ICollection<Grade> grades = new HashSet<Grade>();
            if (teacher != null)
            {
                ICollection<Subject> subjects = teacher.subjects;
                if (subjects != null)
                {

                    foreach (Subject subject in subjects)
                    {
                        grades.Add(subject.grade);
                    }
                }
            }
            return grades;
        }
    }
}
