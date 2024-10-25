using LuminaGed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdAsync(string id);
        Task UpdateAsync(TEntity entity);
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task GetByIdAsync(Func<object, bool> value);
        Task<User> GetByIdAsync(object studentId);
        Task<IEnumerable<object>> GetAsync(Func<object, bool> value);
    }
}
