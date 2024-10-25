using Azure.Core;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Infrastructure.Persistence
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IGenericRepository<Session> _sessionRepo;
        private readonly IGenericRepository<Attendance> _attendanceRepo;
        private readonly IGenericRepository<User> _userRepo;
        private readonly LuminaAppContext _context;

        public AttendanceService(LuminaAppContext context, IGenericRepository<Session> sessionRepo,IGenericRepository<Attendance> attendanceRepo,IGenericRepository<User> userRepo)
        {
            _sessionRepo = sessionRepo;
            _attendanceRepo = attendanceRepo;
            _userRepo = userRepo;
            _context = context;
        }
        public async Task<Attendance> GetStudentAttendanceBySession(string studentId, int sessionId)
        {
            return await _context.Attendances
                        .FirstOrDefaultAsync(e => e.StudentFK == studentId && e.SessionFK == sessionId);
        }
        public async Task AddAttendance(Attendance attendance, string studentId, int sessionId)
        {
            var student = await _userRepo.GetByIdAsync(studentId);
            var session = await _sessionRepo.GetByIdAsync(sessionId);

            if (student == null)
            {
                throw new Exception($"L'étudiant avec l'ID {studentId} n'a pas été trouvé.");
            }

            if (session == null)
            {
                throw new Exception($"La séance avec l'ID {sessionId} n'a pas été trouvé.");
            }

            var existingAttendance = student.Attendances.FirstOrDefault(x => x.SessionFK == sessionId);

            if (existingAttendance != null)
            {
                existingAttendance.attendanceType = attendance.attendanceType;
                existingAttendance.Observation = attendance.Observation;

                await _attendanceRepo.UpdateAsync(existingAttendance);
            }
            else
            {
                attendance.Student = student;
                attendance.session = session;

                await _attendanceRepo.CreateAsync(attendance);
            }
        }


        public async Task<ICollection<Attendance>> GetAttendancesByStudentAndSemester(string studentId, int semester)
        {
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            User student = await _userRepo.GetByIdAsync(studentId);
            switch (semester)
            {
                case 1:
                    startDate = new DateTime(DateTime.Now.Year, 9, 15);
                    endDate = new DateTime(DateTime.Now.Year, 12, 15);
                    break;
                case 2:
                    startDate = new DateTime(DateTime.Now.Year, 1, 2);
                    endDate = new DateTime(DateTime.Now.Year, 3, 15);
                    break;
                case 3:
                    startDate = new DateTime(DateTime.Now.Year, 4, 1);
                    endDate = new DateTime(DateTime.Now.Year, 7, 30);
                    break;
            }

            if (student == null)
            {
                throw new ArgumentException($"L'étudiant avec l'ID {studentId} n'a pas été trouvé.");
            }
            else if (student.Attendances == null)
            {
                throw new InvalidOperationException($"L'élève '{student.FirstName}' n'a pas encore de présence.");
            }

            return student.Attendances.Where(ev => ev.session.end_hour.Date >= startDate && ev.session.end_hour.Date <= endDate).ToList();
        }
    }


}

