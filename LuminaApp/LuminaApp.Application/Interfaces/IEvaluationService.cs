using LuminaApp.Application.Commons;
using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Interfaces
{
    public interface IEvaluationService
    {
        public Task<ICollection<Evaluation>> GetEvaluationsByStudent(string studentId);
        public Task<ICollection<Evaluation>> GetEvaluationsBySubjectAndStudent(string studentId,int subjectId);
        public Task CreateEvaluation(Evaluation evaluation, int sessionId, string StudentId);
        Task<Evaluation> GetStudentEvaluationBySession(string studentId, int sessionId);
    }
}
