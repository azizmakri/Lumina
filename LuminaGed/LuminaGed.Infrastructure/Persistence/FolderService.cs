using LuminaGed.Application.Commons.Exceptions;
using LuminaGed.Application.Interfaces;
using LuminaGed.Domain.Entities;
using LuminaGed.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace LuminaApp.Infrastructure.Persistence
{
    public class FolderService : IFolderService
    {
        private readonly LuminaGedContext _dbContext;
        private readonly IGenericRepository<Grade> _gradeRepo;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Folder> _folderRepo;
        private readonly UserManager<User> _userManager;


        public FolderService(UserManager<User> userManager,LuminaGedContext context, IGenericRepository<Grade> gradeRepo, IGenericRepository<User> userRepo, IGenericRepository<Folder> folderRepo)
        {
            _dbContext = context;
            _gradeRepo = gradeRepo;
            _userRepo = userRepo;
            _folderRepo = folderRepo;
            _userManager = userManager;
        }

        public async Task AddFolder(Folder folder, string teacherId, int parentFolderId, int gradeId)
        {
            User teacher = await _userRepo.GetByIdAsync(teacherId);
            Folder parentFolder = await _folderRepo.GetByIdAsync(parentFolderId);
            Grade grade = await _gradeRepo.GetByIdAsync(gradeId);

            if (teacher == null)
            {
                throw new Exception($"L'enseignant avec l'ID {teacherId} n'a pas été trouvé.");
            }

            var userRoles = await _userManager.GetRolesAsync(teacher);
            if (userRoles.Contains("Teacher"))
            {
                folder.Teacher = teacher;
                folder.grade = grade;

                if (parentFolder != null)
                {
                    folder.ParentFolder = parentFolder;
                    await UpdateParentFoldersModificationDate(folder);
                }
                await _folderRepo.CreateAsync(folder);
            }
            else
            {
                throw new NotAuthorizedException("L'utilisateur n'est pas autorisé à effectuer cette action.");
            }
        }


        public async Task DeleteFolder(int folderId, string ownerId)
        {
            var folderToDelete = await _folderRepo.GetByIdAsync(folderId);
            if (folderToDelete == null)
            {
                throw new Exception($"Dossier avec ID {folderId} introuvable.");
            }
            else if (folderToDelete.TeacherFK == ownerId)
            {
                await UpdateParentFoldersModificationDate(folderToDelete);
                await DeleteFolderAndContents(folderToDelete);
            }
            else
            {
                throw new NotAuthorizedException("L'utilisateur n'est pas autorisé à effectuer cette action.");
            }
        }
        private async Task UpdateParentFoldersModificationDate(Folder folder)
        {
            while (folder.ParenFolderFK != null)
            {
                folder = await _folderRepo.GetByIdAsync(folder.ParenFolderFK.Value);
                if (folder != null)
                {
                    folder.Modification_Date = DateTime.Now;
                    await _folderRepo.UpdateAsync(folder);
                }
                else
                {
                    break;
                }
            }
        }
        private async Task DeleteFolderAndContents(Folder folder)
        {
            // Delete child folders recursively
            if (folder.Folders != null && folder.Folders.Any())
            {
                foreach (var childFolder in folder.Folders.ToList())
                {
                    await DeleteFolderAndContents(childFolder);
                }
            }

            // Delete documents associated with the folder
            if (folder.Documents != null && folder.Documents.Any())
            {
                _dbContext.Documents.RemoveRange(folder.Documents);
            }

            // Remove the folder from the parent's collection
            if (folder.ParentFolder != null)
            {
                folder.ParentFolder.Folders.Remove(folder);
            }

            // Remove the folder itself
            _dbContext.Folders.Remove(folder);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<Folder> GetFolderById(int folderId)
        {
            try
            {
                // Récupérer les dossiers correspondant à l'utilisateur et à l'ID du dossier parent
                var folder = await _folderRepo.GetByIdAsync(folderId);

                return folder;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de l'extraction des dossiers.", ex);
            }
        }

        public async Task<ICollection<Folder>> GetFolderdByTeacherAndGrade(string teacherId, int gradeId)
        {
            try
            {
                // Récupérer les dossiers correspondant à l'utilisateur et à l'ID du dossier parent
                var allFolders = await _folderRepo.GetAsync();
                Grade grade = await _gradeRepo.GetByIdAsync(gradeId);

                if (grade == null)
                {
                    // Handle the case where the grade is null
                    throw new NullReferenceException("L'objet classe est nul.");
                }

                ICollection<Folder> filteredFolders = allFolders
                    .Where(f => f.TeacherFK == teacherId && f.grade != null && f.grade.GradeId == gradeId && f.ParenFolderFK == null)
                    .ToList();

                return filteredFolders;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Une erreur s'est produite lors de l'extraction des dossiers.", ex);
            }
        }


        public async Task RenameFolder( Folder folder )
        {
            

            try
            {
                // Enregistrer les modifications dans la base de données
                await _folderRepo.UpdateAsync(folder);
            }
            catch (Exception ex)
            {
                // Gérer les erreurs de mise à jour de la base de données
                throw new ApplicationException($"Une erreur s'est produite lors de l'extraction des dossiers.", ex);
            }
        }

        public async Task<ICollection<Folder>> GetFoldersByStudent(string studentId)
        {
            var allFolders = await _folderRepo.GetAsync();
            User Student = await _userRepo.GetByIdAsync(studentId);
            if (Student != null)
            {
                Grade grade = Student.grade;
                ICollection<Folder> filteredFolders = allFolders
                    .Where(f => f.grade != null && f.ParenFolderFK == null && f.grade==Student.grade)
                    .ToList();
                return filteredFolders;
            }
                return null;
            
        }
    }
}


