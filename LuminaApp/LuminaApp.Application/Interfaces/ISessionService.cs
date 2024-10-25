using LuminaApp.Application.Features.SessionFeatures.Dtos;
using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Interfaces
{
    public interface ISessionService
    {
        public Task AddSession(Session session, int subjectId, int classroomId);
        public Task<ICollection<Session>> GetSessionsByStudentAndAate(string studentId,DateTime Sessiondate);
        Task<ICollection<Session>> GetSessionsByUserId(string userId);
        Task<ICollection<Session>> GetSessionsByTeacherId(string teacherId);
        Task<Session> GetSessionsById(int sessionId);
        public Task<ICollection<Session>> GetSessionsByGrade(int gradeId);
        public Task updateSession(Session s);
        public Task DeleteSession(int sessionId);



    }
}
