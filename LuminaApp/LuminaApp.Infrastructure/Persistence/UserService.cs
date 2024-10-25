using LuminaApp.Application.Features.GradeFeatures.Queries.GetGradesByTeacher;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Infrastructure.Persistence
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Session> _sessionRepo;
        private readonly RabbitMQPublisher _rabbitMQPublisher;


        public UserService(RabbitMQPublisher rabbitMQPublisher,IGenericRepository<User> userRepo , IGenericRepository<Session> sessionRepo , IGenericRepository<Subject> subjectRepo)
        {
            _userRepo = userRepo;
            _sessionRepo = sessionRepo;
            _rabbitMQPublisher = rabbitMQPublisher;
        }
        public void NotifyGradeFKChange(string userId, int gradeFK)
        {
            _rabbitMQPublisher.PublishGradeFK(userId, gradeFK);
        }
      
        public async Task<ICollection<User>> getChildsFromParent(string parentId)
        {
            User parent=await _userRepo.GetByIdAsync(parentId);
            if (parent == null)
            {
                throw new Exception($"Le parent avec l'ID {parentId} n'a pas été trouvé.");
            }
            return parent.Students;
        }

        public async Task<ICollection<User>> getStudentsByIdSessions(int sessionId)
        {
            Session session = await _sessionRepo.GetByIdAsync(sessionId);
            if (session == null)
            {
                throw new Exception($"La séance avec l'ID {sessionId} n'a pas été trouvée.");
            }
            if(session.subject == null)
            {
                throw new Exception($"matière avec l'ID {session.SubjectFK} n'a pas été trouvé.");

            }
            Subject subject = session.subject;

            if (subject.grade == null)
            {
                throw new Exception($"classe avec l'ID {session.SubjectFK} n'a pas été trouvé.");

            }
            Grade grade = subject.grade;
            if (grade.students == null)
            {
                throw new Exception($"séance avec liste d'étudiants {grade.students} non trouvée.");

            }

            return grade.students;
        }
    }
}
