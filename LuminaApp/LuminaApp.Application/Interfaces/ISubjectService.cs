using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Interfaces
{
    public interface ISubjectService
    {
        public Task<ICollection<Subject>> GetSubjectsByGrade(int gradeId);
        public Task AddSubject(Subject subject, string teacherId, int gradeId);
        public Task DeleteSubject(int subjectId);

    }
}
