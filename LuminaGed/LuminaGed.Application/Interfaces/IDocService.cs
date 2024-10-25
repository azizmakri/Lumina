using LuminaGed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Interfaces
{
    public interface IDocService
    {
        public Task AddDoc(DocumentEntity doc, string teacherFK, int folderFK);
        public Task DeleteDoc(int documentId);
        public Task<IReadOnlyList<DocumentEntity>> GetAllDocuments();
        public Task<ICollection<DocumentEntity>> GetLatestHomeworks(string studentId);
        public Task<byte[]> GetFileByIdAsync(int documentId);

    }
}
