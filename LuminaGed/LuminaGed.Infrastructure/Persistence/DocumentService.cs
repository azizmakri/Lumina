using LuminaGed.Application.Commons.Exceptions;
using LuminaGed.Application.Interfaces;
using LuminaGed.Domain.Entities;
using LuminaGed.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace LuminaGed.Infrastructure.Persistence
{
    public class DocumentService: IDocService
    {
        private readonly LuminaGedContext _context;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<Folder> _folderRepo;
        private readonly UserManager<User> _userManager;
        private readonly IGenericRepository<DocumentEntity> _docRepo;

        public DocumentService(UserManager<User> userManager, IGenericRepository<DocumentEntity> docRepo, LuminaGedContext context, IGenericRepository<User> userRepo, IGenericRepository<Folder> folderRepo)
        {
            _context = context;
            _userRepo = userRepo;
            _folderRepo = folderRepo;
            _userManager = userManager;
            _docRepo = docRepo;
        }
        public async Task AddDoc(DocumentEntity doc, string teacherId, int folderId)
        {
            User teacher = await _userRepo.GetByIdAsync(teacherId);
            Folder parentfolder = await _folderRepo.GetByIdAsync(folderId);

            if (teacher == null)
            {
                throw new Exception($"L'enseignant avec l'ID {teacherId} n'a pas été trouvé.");
            }
            else if (parentfolder == null)
            {
                doc.teacher = teacher;
            }
            else
            {
                var userRoles = await _userManager.GetRolesAsync(teacher);

                if (userRoles[0] == "Student")
                {
                    if(parentfolder.folderType.Equals(FolderType.ToDo))
                    doc.student = teacher;
                    else throw new NotAuthorizedException("L'utilisateur n'est pas autorisé à effectuer cette action.");
                }
                else
                {
                    doc.teacher = teacher;
                }
                doc.folder = parentfolder;
                parentfolder.Modification_Date = DateTime.Now;
                await UpdateParentFoldersModificationDate(parentfolder);

            }
            await _docRepo.CreateAsync(doc);
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
        public async Task<IReadOnlyList<DocumentEntity>> GetAllDocuments()
        {
            // Retrieve all documents from the repository
            return await _docRepo.GetAsync();
        }
        public async Task DeleteDoc(int documentId  )
        {
            var docToDelete = await _docRepo.GetByIdAsync(documentId);
            if (docToDelete == null)
            {
                throw new Exception($"Le document avec l'ID {documentId} n'a pas été trouvé.");
            }
            var folder = docToDelete.folder;
            folder.Modification_Date= DateTime.Now;
            await UpdateParentFoldersModificationDate(folder);
            await _docRepo.DeleteAsync(docToDelete);
        }
        public async Task<byte[]> GetFileByIdAsync(int documentId)
        {
            var document = await _docRepo.GetByIdAsync(documentId);
            if (document == null)
            {
                throw new Exception($"Le document avec l'ID {documentId} n'a pas été trouvé.");
            }

            var filePath = document.DocumentPath;
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Fichier non trouvé sur le disque", filePath);
            }

            return await System.IO.File.ReadAllBytesAsync(filePath);
        }

        public async Task<ICollection<DocumentEntity>> GetLatestHomeworks(string studentId)
        {
            User student = await _userRepo.GetByIdAsync(studentId);

            if (student?.grade?.Folders == null)
            {
                return new List<DocumentEntity>();
            }

            var result = student.grade.Folders
                .Where(folder => folder.Documents != null)
                .SelectMany(folder => folder.Documents)
                .Where(doc => doc.teacher != null && doc.folder.folderType == FolderType.ToDo)
                .OrderByDescending(doc => doc.Creation_date)
                .Take(5)
                .ToList();

            return result;
        }
    }
}
