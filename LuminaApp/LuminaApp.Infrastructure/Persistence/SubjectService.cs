using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Infrastructure.Persistence
{
    public class SubjectService : ISubjectService
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Subject> _subjectRepo;
        private readonly IGenericRepository<Grade> _gradetRepo;

        public SubjectService(IGenericRepository<User> userRepo, IGenericRepository<Subject> subjectRepo, IGenericRepository<Grade> gradetRepo)
        {
            _userRepo = userRepo;
            _subjectRepo = subjectRepo;
            _gradetRepo = gradetRepo;
        }

        public async Task AddSubject(Subject subject, string teacherId, int gradeId)
        {
            User teacher= await _userRepo.GetByIdAsync(teacherId);
            Grade grade= await _gradetRepo.GetByIdAsync(gradeId);
            if (teacher!=null && grade!=null) 
            {
                subject.grade = grade;
                subject.teacher = teacher;
                await _subjectRepo.CreateAsync(subject);
            }
        }

        public async Task DeleteSubject(int subjectId)
        {
            var subject=await _subjectRepo.GetByIdAsync(subjectId);
            await _subjectRepo.DeleteAsync(subject);
        }

        public async Task<ICollection<Subject>> GetSubjectsByGrade(int gradeId)
        {
            Grade grade= await _gradetRepo.GetByIdAsync(gradeId);
            if(grade != null) 
            {
                return grade.subjects;
            }
            else
            {
                throw new Exception($"le classe avec l'identifiant : {gradeId} n'a pas été trouvé ");
            }
        }
    }
}
