using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Infrastructure.Persistence
{
    public class ClassRoomService : IClassRoomService
    {
         private readonly IGenericRepository<ClassRoom> _classRepo;

        public ClassRoomService(IGenericRepository<ClassRoom> classRepo)
        {
            _classRepo = classRepo;
           
        }
        public async Task<IReadOnlyList<ClassRoom>> GetAllClasses()
        {
            IReadOnlyList<ClassRoom> classrooms = await _classRepo.GetAsync();
            return classrooms;
        }
     
        public async Task AddClassroom(ClassRoom classRoom)
        {
            
            {
             
              
                await _classRepo.CreateAsync(classRoom);
            }
        }
        public async Task DeleteClassroom(int classroomId)
        {
            try
            {
                var classroom = await _classRepo.GetByIdAsync(classroomId);

                if (classroom != null)
                {
                    await _classRepo.DeleteAsync(classroom);
                }
                else
                {
                    throw new ArgumentException($"La salle de classe avec l'ID {classroomId} n'a pas été trouvée.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression d'une salle", ex);
            }
        }

    }
}
