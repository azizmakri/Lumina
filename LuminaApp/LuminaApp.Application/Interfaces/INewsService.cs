using LuminaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Interfaces
{
    public interface INewsService
    {
        public  Task AddNews(SchoolNews news, string adminId);

        public Task<IReadOnlyList<SchoolNews>> GetAllNews();

        public  Task<byte[]> GetNewsByIdAsync(int NewsId);
    }
}
