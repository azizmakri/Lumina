using LuminaApp.Application.Features.SessionFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Reflection.Metadata;

namespace LuminaApp.Infrastructure.Persistence
{
    public class SessionService : ISessionService
    {
        private readonly IHubContext<NewsNotificationHub, INotificationHub> _newsNotification;
        private readonly IGenericRepository<Grade> _gradeRepo;
        private readonly IGenericRepository<ClassRoom> _classRepo;
        public IGenericRepository<User> _userRepo;
        public IGenericRepository<Subject> _subjectRepo;
        public IGenericRepository<Session> _sessionRepo;
        public SessionService(IHubContext<NewsNotificationHub, INotificationHub> hubContext,IGenericRepository<Grade> gradeRepo,IGenericRepository<ClassRoom> classRepo, IGenericRepository<User> userRepo, IGenericRepository<Subject> subjectRepo, IGenericRepository<Session> sessionRepo)
        {
            _gradeRepo = gradeRepo;
            _classRepo = classRepo;
            _userRepo = userRepo;
            _subjectRepo = subjectRepo;
            _sessionRepo = sessionRepo;
            _newsNotification = hubContext;
        }

        public async Task AddSession(Session session, int subjectId, int classroomId)
        {
            Subject subject = await _subjectRepo.GetByIdAsync(subjectId);
            ClassRoom classRoom = await _classRepo.GetByIdAsync(classroomId);

            if (subject == null)
            {
                throw new Exception($"Matière avec l'ID {subjectId} n'a pas été trouvée.");
            }
            if (classRoom == null)
            {
                throw new Exception($"La salle avec l'ID {classroomId} n'a pas été trouvée.");
            }

            if (session.start_hour >= session.end_hour)
            {
                throw new Exception("L'heure de début de la séance doit être avant l'heure de fin.");
            }

            Grade grade = subject.grade;
            ICollection<Subject> subjectList = grade.subjects;

            var intersectingSessions = subjectList
                .SelectMany(sub => sub.sessions ?? new List<Session>())
                .Where(s =>
                    (session.start_hour < s.end_hour && session.end_hour > s.start_hour)
                ).ToList();

            if (intersectingSessions.Any())
            {
                throw new Exception("Cette classe a déjà une séance à cette heure.");
            }

            var classroomSessions = classRoom.Session ?? new List<Session>();
            var intersectingClassroomSessions = classroomSessions
                .Where(s =>
                    (session.start_hour < s.end_hour && session.end_hour > s.start_hour)
                ).ToList();

            // If there are intersecting sessions, throw an exception
            if (intersectingClassroomSessions.Any())
            {
                throw new Exception("Cette salle est déjà occupée à cette heure.");
            }

            // Send notification
            await _newsNotification.Clients.All.SendMessage(new NotificationEntity
            {
                Content = "Notification session",
                Timestamp = DateTime.Now,
            });

            // Assign the subject and classroom to the session
            session.subject = subject;
            session.ClassRoom = classRoom;

            // Create the session
            await _sessionRepo.CreateAsync(session);
        }


        public async Task<ICollection<Session>> GetSessionsByStudentAndAate(string studentId, DateTime sessionDate)
        {
            try
            {
                User student = await _userRepo.GetByIdAsync(studentId);
                Grade grade = student.grade;
                var sessions = new List<Session>();
                if (grade == null)
                {
                    return null;
                }
                var subjects = grade.subjects;
                foreach (Subject s in subjects)
                {
                    foreach (Session ss in s.sessions)
                    {
                        // Check if session date matches the provided date
                        if (ss.start_hour.Day == sessionDate.Day)
                        {
                            sessions.Add(ss);
                        }
                    }
                }
                return sessions;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de la recherche des séances.", ex);
            }
        }

        public async Task<ICollection<Session>> GetSessionsByUserId(string userId)
        {
            try
            {
                User user = await _userRepo.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new Exception($"L'utilisateur avec l'ID {userId} n'a pas été trouvé.");
                }
                var sessions = new List<Session>();

                if (user.grade?.subjects != null)
                {
                    foreach (var subject in user.grade.subjects)
                    {
                        sessions.AddRange(subject.sessions);
                    }
                }

                return sessions.Count > 0 ? sessions : null;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de la recherche des séances.", ex);
            }
        }
        public async Task<ICollection<Session>> GetSessionsByTeacherId(string teacherId)
        {
            try
            {
                User user = await _userRepo.GetByIdAsync(teacherId);
                if (user == null)
                {
                    throw new Exception($"L'enseignant avec l'ID {teacherId} n'a pas été trouvé.");
                }

                var sessions = new List<Session>();

                if (user.subjects != null)
                {
                    foreach (var subject in user.subjects)
                    {
                        sessions.AddRange(subject.sessions);
                    }
                }

                return sessions.Count > 0 ? sessions : null;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de la recherche des séances.", ex);
            }
        }

        public async Task<ICollection<Session>> GetSessionsByGrade(int gradeId)
        {
            Grade grade = await _gradeRepo.GetByIdAsync(gradeId);
            ICollection<Session> sessions = new List<Session>();
            if (grade != null)
            {
                var subjects = grade.subjects;
                foreach (var subject in subjects)
                {
                    foreach (var session in subject.sessions)
                    {
                        sessions.Add(session);
                    }
                }
                return sessions;
            }
            else
            {
                throw new Exception($"le classe avec l'identifiant : {gradeId} n'a pas été trouvé ");
            }
        }

        public async Task updateSession(Session sessionToUpdate)
        {
            try
            {
                await _sessionRepo.UpdateAsync(sessionToUpdate);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de la mise à jour de la séance.", ex);
            }
        }

        public async Task<Session> GetSessionsById(int sessionId)
        {
            try
            {
                Session session = await _sessionRepo.GetByIdAsync(sessionId);
                return session;
            }catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de la recherche de la séance.", ex);
            }
        }

        public async Task DeleteSession(int sessionId)
        {
            Session session = await _sessionRepo.GetByIdAsync(sessionId);
            if (session == null)
            {
                throw new Exception($"Séance avec l'ID {sessionId} n'a pas été trouvée.");
            }
            await _sessionRepo.DeleteAsync(session);
        }
    }


    }
