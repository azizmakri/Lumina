using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Interfaces
{
    public interface IUserService
    {
        public Task<ICollection<User>> getChildsFromParent(string parentId);
        public Task<ICollection<User>> getStudentsByIdSessions(int sessionId);

    }
}
