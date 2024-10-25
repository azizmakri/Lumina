using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Interfaces
{
    public interface IClassRoomService
    {
        public Task<IReadOnlyList<ClassRoom>> GetAllClasses();
        public Task AddClassroom(ClassRoom ClassRoom);

        public Task DeleteClassroom(int classroomId);



    }
}
