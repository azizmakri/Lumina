using LuminaGed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Interfaces
{
    public interface IFolderService
    {
        public Task AddFolder(Folder folder , string teacherFK, int parentFolderFK,int gradeId);
        public Task<Folder> GetFolderById(int folderId);
        public Task<ICollection<Folder>> GetFolderdByTeacherAndGrade(string teacherId,int gradeId);
        public Task<ICollection<Folder>> GetFoldersByStudent(string studentId);
        public Task RenameFolder(Folder folder);
        public Task DeleteFolder(int folderId,string ownerId);


    }
}


