using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace LuminaApp.Infrastructure.Persistence
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IGenericRepository<Subject> _subjectRepo;
        private readonly IGenericRepository<Session> _sessionRepo;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Evaluation> _evaluationRepo;
        private readonly LuminaAppContext _context;

        public EvaluationService(LuminaAppContext context, IGenericRepository<Subject> subjectRepo, IGenericRepository<Session> sessionRepo, IGenericRepository<User> userRepo, IGenericRepository<Evaluation> evaluationRepo)
        {
            _subjectRepo = subjectRepo;
            _sessionRepo = sessionRepo;
            _userRepo = userRepo;
            _evaluationRepo = evaluationRepo;
            _context = context;
        }
        public async Task<Evaluation> GetStudentEvaluationBySession(string studentId, int sessionId)
        {
            return await _context.Evaluations
                        .FirstOrDefaultAsync(e => e.studentFk == studentId && e.sessionFk == sessionId);
        }

        public async Task CreateEvaluation(Evaluation evaluation, int sessionId, string studentId)
        {
            Session session = await _sessionRepo.GetByIdAsync(sessionId);
            User student = await _userRepo.GetByIdAsync(studentId);

            if (session == null)
            {
                throw new Exception($"La session avec l'ID {sessionId} n'a pas été trouvée.");
            }
            else if (student == null)
            {
                throw new Exception($"L'étudiant avec l'ID {studentId} n'a pas été trouvé.");
            }
            else
            {
                // Check if there's already an evaluation for this session and student
                Evaluation existingEvaluation = student.evaluations
                    .FirstOrDefault(x => x.sessionFk == sessionId);

                if (existingEvaluation != null)
                {
                    // Update the existing evaluation with the new data
                    existingEvaluation.Mark = evaluation.Mark;
                    await _evaluationRepo.UpdateAsync(existingEvaluation); // Assume UpdateAsync method exists
                }
                else
                {
                    // Create a new evaluation
                    evaluation.session = session;
                    evaluation.student = student;

                    await _evaluationRepo.CreateAsync(evaluation);
                }
            }
        }

        public async Task<ICollection<Evaluation>> GetEvaluationsByStudent(string studentId)
              {
                  User student = await _userRepo.GetByIdAsync(studentId);
                  if (student == null)
                  {
                      throw new ArgumentException($"L'étudiant avec l'ID {studentId} n'a pas été trouvé.");
                  }
                  if (student.evaluations == null)
                  {
                      throw new InvalidOperationException($"L'étudiant '{student.FirstName}' n'a pas encore d'évaluation.");
                  }
                  return student.evaluations;
              }

        public async Task<ICollection<Evaluation>> GetEvaluationsBySubjectAndStudent(string studentId, int subjectId)
        {
            Subject subject = await _subjectRepo.GetByIdAsync(subjectId);
            User student = await _userRepo.GetByIdAsync(studentId);
            ICollection<Evaluation> evaluations=new List<Evaluation>();
            if (student != null && subject != null) 
            {
                foreach(var session in subject.sessions)
                {
                    if (session.evaluations != null)
                    {
                        foreach (var evaluation in session.evaluations)
                        {
                            if(evaluation.student == student)
                            {
                                evaluations.Add(evaluation);
                            }
                        }
                    }
                }
            }
            return evaluations;
        }
    }
}
